import { HubConnectionState } from "@microsoft/signalr";
import { NotificationTypes } from "../notifying/Notification";
import { useConnection } from "./SignalRConnection";
import { useNavigate } from "react-router-dom";
import { useCallback, useState } from "react";
import { NodeGraphEvent, NodeGraphEvents } from "../nodes/NodeGraphEvents";
import { SyncSnapshot } from "../nodes/NodeGraphSync";

interface SessionProps {
    url: string,
    onMessage: (message: string, type: NotificationTypes) => void,
}


export const useSession = ({url, onMessage }: SessionProps) => {
    
    const {useMethod, useStateHandler, useSubscribe} = useConnection(url);
    const navigate = useNavigate();
    const [version, setVersion] = useState(0);

    const synchronizeRequest = useMethod<number, SyncSnapshot>("Synchronize");

    const synchronize = useCallback(async () => {
        const result = await synchronizeRequest(version);
        
        

    }, [version, synchronizeRequest]);

    useStateHandler((state) => {
        if (state === HubConnectionState.Connected) {
            onMessage("Connected", NotificationTypes.message);
            synchronize();
        }
    });

    useSubscribe("SendMessageAsync", (message: string) => {
        onMessage(message, NotificationTypes.message);
    });

    useSubscribe("ShutdownAsync", (reason: string) => {
        onMessage(reason, NotificationTypes.warning);
        navigate("/");
    });

    useSubscribe("OnNodeGraphChanged", (event: NodeGraphEvent) => {
        if (event.version !== version + 1) {
            synchronize();
            return;
        }
    });

    return {
        
    };
}