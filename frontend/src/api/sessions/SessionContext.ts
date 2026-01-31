import { createContext } from "react";


export interface SignalRContextInterface {
    hub: signalR.HubConnection | null,
    state: signalR.HubConnectionState
}

export const SignalRContext = createContext<SignalRContextInterface | null>(null);