import { HubConnectionState } from "@microsoft/signalr";
import { NotificationTypes } from "../notifying/Notification";
import { useConnection } from "./SignalRConnection";

interface SessionProps {
    url: string,
    onMessage: (message: string, type: NotificationTypes) => void
}


export const useSession = ({url, onMessage}: SessionProps) => {
    
    const {useMethod, useStateHandler, useSubscribe} = useConnection(url);


    useStateHandler((state) => {

        if (state === HubConnectionState.Disconnected) {
            onMessage("Server disconnected", NotificationTypes.error);
        }
        else if (state === HubConnectionState.Connected) {
            onMessage("Connected", NotificationTypes.message);
        }
    });

    return {
        
    };
}