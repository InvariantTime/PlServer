import { MouseEvent, useCallback, useEffect, useRef, useState } from "react"
import { NodeDefinition } from "../api/nodes/NodeDefinition";
import { NodeGraphAdapter } from "../api/nodes/NodeGraphAdapter";


export const useNodeSystem = (adapter: NodeGraphAdapter) => {

    const addNode = useCallback((node: NodeDefinition) => {
       // adapter.addConnection();
    }, []);

    const removeNode = useCallback((nodeId: string) => {

    }, []);

    const moveNode = useCallback((nodeId: string, x: number, y: number) => {
        const node = adapter.nodes.find(x => x.id === nodeId);
        
        if (node === undefined)
            return;
        
        node.position = {x: x, y: y};
        
       /* setNodes(prev => {

            const nodes = prev.filter(x => x.id !== node.id);

            return [...nodes, node];
        });*/
    }, []);

    const createEdge = useCallback((source: {nodeId: string, pinId: string}, target: {nodeId: string, pinId: string}) => {

        const connection = {
            source: source,
            target: target,
            id: crypto.randomUUID()
        };

        adapter.addConnection();

    }, [adapter.nodes]);

    const removeEdge = useCallback((id: string) => {
       
    }, [adapter.nodes]);

    return {
        nodeDefinitions: adapter.definitions,
        nodes: adapter.nodes,
        connections: adapter.connections,
        addNode,
        removeNode,
        moveNode,
        createEdge,
        removeEdge
    };
}