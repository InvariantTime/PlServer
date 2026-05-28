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

const num1 : NodeDefinition = {
    id: "num",
    name: "Integer",
    isFinal: false,
    inputs: [], 
    outputs: [{id: "5fab6c67-2627-4b24-8fe4-b34cb68b0fe6", name: "number", type: "output"}]
};

const sqrt : NodeDefinition = {
    id: "sqrt",
    name: "Integer Sqrt",
    isFinal: false,
    inputs: [{id: "9e7c0e27-48f5-471e-8a03-9cf44ad93770", name: "number", type: "input"}],
    outputs: [{id: "5fab6c67-2627-4b24-8fe4-b34cb68b0fe6", name: "result", type: "output"}]
};

const message : NodeDefinition = {
    id: "print",
    name: "Print",
    isFinal: true,
    inputs: [{id: "9e7c0e27-48f5-471e-8a03-9cf44ad93770", name: "message", type: "input"}], 
   //     {id: "b79958ba-8e48-486c-a5e5-ac4d2771761f", name: "input 2", type: "input"}],
   // outputs: [{id: "5fab6c67-2627-4b24-8fe4-b34cb68b0fe6", name: "output", type: "output"}]
   outputs: []
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
            definitions: [num1, message, sqrt],
            handleCommand: handleCommandRequest
        }
    };
}