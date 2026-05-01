import { HubConnectionState } from "@microsoft/signalr";
import { NotificationTypes } from "../notifying/Notification";
import { useConnection } from "./SignalRConnection";
import { useNavigate } from "react-router-dom";

interface SessionProps {
    url: string,
    onMessage: (message: string, type: NotificationTypes) => void,
}


export const useSession = ({url, onMessage }: SessionProps) => {
    
    const {useMethod, useStateHandler, useSubscribe} = useConnection(url);
    const navigate = useNavigate();

    useStateHandler((state) => {

        if (state === HubConnectionState.Connected) {
            onMessage("Connected", NotificationTypes.message);
        }
    });

    useSubscribe("SendMessageAsync", (message: string) => {
        onMessage(message, NotificationTypes.message);
    });

    useSubscribe("ShutdownAsync", (reason: string) => {
        onMessage(reason, NotificationTypes.warning);
        navigate("/");
    });

    return {
        
    };
}