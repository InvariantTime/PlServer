import { HubConnection, HubConnectionBuilder, HubConnectionState } from "@microsoft/signalr";
import { useCallback, useEffect, useRef } from "react";

interface Connection {

    useSubscribe: (name: string, callback: (arg: any) => Promise<any>) => void,
    useMethod: (name: string) => void,
    useStateHandler: (callback: (state: HubConnectionState) => void) => void
}

type Subscribe = {
    name: string,
    callback: (arg: any) => Promise<any>
}

type StateHandler = (state: HubConnectionState) => void

export const useConnection = (url: string): Connection => {

    const subscribesRef = useRef<Subscribe[]>([]);
    const handlers = useRef<StateHandler[]>([]);
    const hubRef = useRef<HubConnection | null>(null);
    
    const notifyStateChanged = useCallback((state: HubConnectionState) => {
        handlers.current.forEach((handler) => {
            handler(state);
        });

    }, [handlers]);

    useEffect(() => {
        const builder = new HubConnectionBuilder()
            .withUrl(url)
            .withAutomaticReconnect();

        hubRef.current = builder.build();
        hubRef.current.start()
            .then(() => {
                subscribesRef.current.forEach((sub) => {
                    hubRef.current?.on(sub.name, sub.callback);
                });

                notifyStateChanged(hubRef.current?.state ?? HubConnectionState.Disconnected);

                hubRef.current?.onclose(() => notifyStateChanged(HubConnectionState.Disconnected));
                hubRef.current?.onreconnected(() => notifyStateChanged(HubConnectionState.Connected));
                hubRef.current?.onreconnecting(() => notifyStateChanged(HubConnectionState.Reconnecting));
            })
            .catch(() => {
                notifyStateChanged(HubConnectionState.Disconnected);
            });

        return () => {
            hubRef.current?.stop();
        }
    }, []);


    const useSubscribe = (name: string, callback: (arg: any) => Promise<any>) => {

        useEffect(() => {
            hubRef.current?.on(name, callback);
            subscribesRef.current = [...subscribesRef.current, { name: name, callback: callback }];

            return () => {
                subscribesRef.current = subscribesRef.current.filter(x => x.name !== name || x.callback !== callback);
                hubRef.current?.off(name, callback);
            }
        }, []);
    };

    const useMethod = <T>(name: string): (arg: T) => Promise<any> => {

        const invoker = useCallback((arg: T) => {
            return hubRef.current?.invoke(name, arg) ?? Promise.resolve();
        }, []);

        return invoker;
    };

    const useStateHandler = (callback: (state: HubConnectionState) => void) => {

        useEffect(() => {

            handlers.current = [...handlers.current, callback];

            return () => {
                handlers.current = handlers.current.filter(x => x !== callback);
            };
        }, [])
    };

    return {
        useSubscribe: useSubscribe,
        useMethod: useMethod,
        useStateHandler: useStateHandler
    };
}