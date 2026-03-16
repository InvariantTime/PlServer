import { MouseEvent, useCallback, useEffect, useRef, useState } from "react"
import { NodeDefinition } from "../api/nodes/NodeDefinition";
import { NodeViewport } from "../components/nodeSystem/NodeViewport";
import { NodeConnection } from "../api/nodes/NodeConnection";
import { NodeInstance } from "../api/nodes/NodeInstance";


export const useNodeSystem = () => {

    const [nodeDefinitions, setNodeDefinitions] = useState<NodeDefinition[]>([
        {
            id: "AFfdsfdsfdsf",
            inputs: [{ id: "dsadas", name: "pin 1", type: "input" }, { id: "dsdas", name: "pin 2", type: "input" }],
            outputs: [{ id: "vvv", name: "output", type: "output" }]
        }
    ]);

    const [nodes, setNodes] = useState<NodeInstance[]>([

        {
            collapsed: false,
            definitionId: "AFfdsfdsfdsf",
            id: "cccmmmvdvd",
            name: "My node",
            position: { x: 100, y: 400 },
            values: []
        },
        {
            collapsed: false,
            definitionId: "AFfdsfdsfdsf",
            id: "vncvmdfdsfdsfnm",
            name: "My node",
            position: { x: 600, y: 300 },
            values: []
        }
    ]);

    const [connections, setConnections] = useState<NodeConnection[]>([
        {id: "jfsdlkdsjf", source: {nodeId: "vncvmdfdsfdsfnm", pinId: "vvv"}, target: {nodeId: "cccmmmvdvd", pinId: "dsdas"}}
    ]);

    const addNode = useCallback((node: NodeDefinition) => {

    }, []);

    const removeNode = useCallback((nodeId: string) => {

    }, []);

    const moveNode = useCallback((nodeId: string, x: number, y: number) => {
        const node = nodes.find(x => x.id === nodeId);
        
        if (node === undefined)
            return;
        
        node.position = {x: x, y: y};
        
        setNodes(prev => {

            const nodes = prev.filter(x => x.id !== node.id);

            return [...nodes, node];
        });
    }, []);

    const createEdge = useCallback((source: {nodeId: string, pinId: string}, target: {nodeId: string, pinId: string}) => {

        const connection = {
            source: source,
            target: target,
            id: crypto.randomUUID()
        };

        setConnections(prev => [...prev, connection]);

    }, [connections]);

    const removeEdge = useCallback((id: string) => {
        setConnections(prev => prev.filter(x => x.id != id));
    }, [connections]);

    return {
        nodeDefinitions,
        nodes,
        connections,
        addNode,
        removeNode,
        moveNode,
        createEdge,
        removeEdge
    };
}