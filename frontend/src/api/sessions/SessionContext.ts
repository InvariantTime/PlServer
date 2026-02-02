import { HubConnection } from "@microsoft/signalr";
import { createContext, use, useContext, useEffect, useRef } from "react";


export interface ServiceConnection
{
    hub: HubConnection | null,
    state: signalR.HubConnectionState,
    id: string | null,
    error: Error | null
}

export interface SessionContextType {
    connection: ServiceConnection
}

export const SessionContext = createContext<SessionContextType | null>(null);

export const useSession = () : SessionContextType => {
    const session = useContext(SessionContext);
    
    if (session == null)
        throw new Error("SessionProvider is not initialized");

    return session;
}

export function useListen<T = any>(name: string, callback: (data: T) => void) {
    const session = useSession();

    const callbackRef = useRef(callback);

    useEffect(() => {
        callbackRef.current = callback;
    }, [callback]);

    useEffect(() => {
        const handler = (...args: any[]) => {
            callbackRef.current(args.length === 1 ? args[0] : args);
        };

        session.connection.hub?.on(name, handler);

        return () => {
            session.connection.hub?.off(name, callback);
        };
    }, [session.connection.hub, session.connection.state, name]);
}

export function useInvoke<T = any>(name: string)
{
    
}