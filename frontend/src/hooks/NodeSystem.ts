import { useCallback, useEffect, useState } from "react"
import { NodeGraphAdapter } from "../api/nodes/NodeGraphAdapter";
import { NodeDefinition } from "../api/nodes/NodeDefinition";
import { NodeInstance } from "../api/nodes/NodeInstance";
import throttle from 'lodash.throttle';


export const useNodeSystem = (adapter: NodeGraphAdapter) => {

    const [nodes, setNodes] = useState<NodeInstance[]>([]);
    const [lockedNode, setNodeLock] = useState<string | null>(null);

    useEffect(() => {
        setNodes(prev => {

            if (lockedNode !== null)
            {
                var locked = adapter.nodes.find(x => x.id === lockedNode);

                if (locked === undefined) {
                    setNodeLock(null);
                    return adapter.nodes;
                }

                var other = adapter.nodes.filter(x => x.id !== lockedNode);
                var position = prev.find(x => x.id === lockedNode)?.position ?? {x: 0, y: 0};

                var currentLocked : NodeInstance = {
                    id: locked!.id, position: position!, 
                    name: locked!.name, 
                    values: locked!.values, 
                    definitionId: locked!.definitionId,
                    collapsed: false
                };

                return [...other, currentLocked];
            }

            return adapter.nodes;
        });

    }, [adapter.nodes, adapter, adapter.connections, adapter.definitions]);

    const addNode = useCallback(async (position: {x: number, y: number}, definitionId: string) => {
       await adapter.handleCommand({type: "add_node", definition: definitionId, position: position});
    }, []);

    const removeNode = useCallback(async (nodeId: string) => {
        await adapter.handleCommand({type: "remove_node", nodeId: nodeId});
    }, []);

    const moveNode = useCallback(async (nodeId: string, x: number, y: number) => {
        var node = nodes.find(x => x.id === nodeId);

        if (node === undefined)
            return;

        node.position = {x: x, y: y};

        setNodes(prev => {

            var other = prev.filter(x => x.id !== nodeId);
            return [...other, node!];
        });

        throttle(async () => {
            await adapter.handleCommand({type: "move_node", nodeId: nodeId, position: {x: x, y: y}});
        }, 200,  { trailing: false });
    }, [nodes, adapter.handleCommand]);

    const createEdge = useCallback((source: {nodeId: string, pinId: string}, target: {nodeId: string, pinId: string}) => {

        const connection = {
            source: source,
            target: target,
            id: crypto.randomUUID()
        };

    }, []);

    const removeEdge = useCallback((id: string) => {
       
    }, [adapter.nodes]);

    return {
        nodeDefinitions: adapter.definitions,
        nodes: nodes,
        connections: adapter.connections,
        addNode,
        removeNode,
        moveNode,
        createEdge,
        removeEdge
    };
}