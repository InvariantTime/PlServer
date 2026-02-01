import { HttpTransportType, LogLevel } from "@aspnet/signalr";
import { HubConnection, HubConnectionBuilder, HubConnectionState } from "@microsoft/signalr";
import { useEffect, useMemo, useRef, useState } from "react";
import { SessionContext } from "../../api/sessions/SessionContext";

export interface SessionProviderProps
{
    children: React.ReactNode
    url: string
}

export const SessionProvider = ({children, url}: SessionProviderProps) => {

    const connectionRef = useRef<HubConnection>(null);
    const [state, setState] = useState<HubConnectionState>(HubConnectionState.Disconnected);
    const [connectionId, setConnectionId] = useState<string | null>(null);
    const [error, setError] = useState<Error | null>(null);

    useEffect(() =>
    {
        const connection = new HubConnectionBuilder()
            .withUrl(url, HttpTransportType.WebSockets | HttpTransportType.ServerSentEvents)
            .withAutomaticReconnect()
            .configureLogging(LogLevel.Information)
            .build();

        connectionRef.current = connection;

        connection.onreconnecting(() => {
            console.warn("[Sessions]: reconnecting");
            setState(connection.state);
        });

        connection.onreconnected(() => {
            console.warn("[Sessions]: reconnected");
            setState(connection.state);
            setConnectionId(connection.connectionId);
        })

        connection.onclose(() => {
            console.warn("[Sessions]: disconnected")
        });

        connection.start().then(() => {
            setConnectionId(connection.connectionId);
            setState(connection.state);
        });

        return () => {
            connection.stop().catch(console.error);
        };

    }, [url])


    const value = useMemo(() => ({
        connection: {hub: connectionRef.current, state: state, id: connectionId, error }
    }), [state, error]);

    return (
        <SessionContext.Provider value={value}>
            {children}
        </SessionContext.Provider>
    );
}