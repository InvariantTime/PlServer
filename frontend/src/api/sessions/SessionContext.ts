import { HubConnection } from "@microsoft/signalr";
import { createContext, useContext } from "react";


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

export const useListen = () => {

}