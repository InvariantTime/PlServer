import { HubConnectionState } from "@microsoft/signalr";
import { NotificationTypes } from "../notifying/Notification";
import { useConnection } from "./SignalRConnection";
import { useNavigate } from "react-router-dom";
import { useCallback, useState } from "react";
import { NodeGraphEvent, NodeGraphEvents } from "../nodes/NodeGraphEvents";
import { SyncSnapshot } from "../nodes/NodeGraphSync";
import { NodeGraphAdapter, NodeGraphCommand } from "../nodes/NodeGraphAdapter";
import { NodeInstance } from "../nodes/NodeInstance";
import { NodeConnection } from "../nodes/NodeConnection";
import { NodeDefinition } from "../nodes/NodeDefinition";

interface SessionProps {
    url: string,
    onMessage: (message: string, type: NotificationTypes) => void,
}

interface Session {
    adapter: NodeGraphAdapter
}

const definition : NodeDefinition = {
    id: "abc",
    inputs: [{id: "9e7c0e27-48f5-471e-8a03-9cf44ad93770", name: "input 1", type: "input"}, 
        {id: "b79958ba-8e48-486c-a5e5-ac4d2771761f", name: "input 2", type: "input"}],
    outputs: [{id: "5fab6c67-2627-4b24-8fe4-b34cb68b0fe6", name: "output", type: "output"}]
};


export const useSession = ({url, onMessage }: SessionProps) : Session => {
    
    const [nodes, setNodes] = useState<NodeInstance[]>([]);
    const [connections, setConnections] = useState<NodeConnection[]>([]);
    const [nodeDefinitions, setNodeDefinitions] = useState<NodeDefinition[]>([]);

    const {useMethod, useStateHandler, useSubscribe} = useConnection(url);
    const navigate = useNavigate();
    const [version, setVersion] = useState(0);

    const synchronizeRequest = useMethod<number, SyncSnapshot>("Synchronize");
    const handleCommandRequest = useMethod<NodeGraphCommand, void>("HandleCommand");

    const synchronize = useCallback(async () => {
        const result = await synchronizeRequest(version);

        if (result.type === "full") {
            setNodes(result.nodes);
            setConnections(result.connections);
        }
        else if (result.type === "delta") {

        }

    }, [version, synchronizeRequest]);

    useStateHandler((state) => {
        if (state === HubConnectionState.Connected) {
            onMessage("Connected", NotificationTypes.message);
            synchronize();
            handleCommandRequest({type: "add_node", position: {x: 100, y: 200}, definition: "aaa"});
        }
    });

    useSubscribe("SendMessageAsync", (message: string) => {
        onMessage(message, NotificationTypes.message);
    });

    useSubscribe("ShutdownAsync", (reason: string) => {
        onMessage(reason, NotificationTypes.warning);
        navigate("/");
    });

    useSubscribe("SendEventAsync", async (event: NodeGraphEvent) => {
        await synchronize();
    });

    return {
        adapter: {
            nodes: nodes,
            connections: connections,
            definitions: [definition],
            handleCommand: handleCommandRequest
        }
    };
}