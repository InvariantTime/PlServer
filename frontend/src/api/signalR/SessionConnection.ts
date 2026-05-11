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
    inputs: [{id: "", name: "input 1", type: "input"}, {id: "", name: "input 2", type: "input"}],
    outputs: [{id: "", name: "output", type: "output"}]
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

    useSubscribe("OnNodeGraphChanged", (event: NodeGraphEvent) => {
        if (event.version !== version + 1) {
            synchronize();
            return;
        }
    });

    const addNode = useCallback((position: {x: number, y: number}, definition: string) => {

        handleCommandRequest({type: "add_node", position: position, definition: definition});
    }, [handleCommandRequest]);

    return {
        adapter: {
            nodes: nodes,
            connections: connections,
            definitions: [definition],
            addConnection: () => {},
            addNode: addNode,
            removeConnection: () => {},
            removeNode: () => {}
        }
    };
}