import { MouseEvent, useCallback, useEffect, useRef, useState } from "react"
import { NodeDefinition } from "./NodeDefinition";
import { NodeViewport } from "../../components/nodeSystem/NodeViewport";
import { NodeConnection } from "./NodeConnection";
import { NodeInstance } from "./NodeInstance";


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


    const [viewport, setViewport] = useState<NodeViewport>({ x: 0, y: 0, zoom: 1 });


    const [pinRefs, setPinRefs] = useState<Record<string, HTMLDivElement | null>>({});
    const canvasRef = useRef<HTMLDivElement>(null);

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

    const registerPinRef = useCallback((nodeId: string, pinId: string, element: HTMLDivElement) => {
        const id = `${nodeId}_${pinId}`
        setPinRefs(prev => {
            prev[id] = element;
            return prev;
        });

    }, [pinRefs]);

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

    const setCanvasRef = useCallback((canvas: HTMLDivElement | null) => {
        canvasRef.current = canvas;
    }, [canvasRef]);

    const moveViewport = useCallback((xOffset: number, yOffset: number) => {
        setViewport(prev => {
            return { x: prev.x + xOffset, y: prev.y + yOffset, zoom: prev.zoom };
        });
    }, []);

    const zoomViewport = useCallback((zoom: number) => {
        setViewport(prev => {
            return { x: prev.x, y: prev.y, zoom: zoom };
        });
    }, []);

    const getPinPosition = useCallback((nodeId: string, pinId: string): { x: number, y: number } | null => {

        const id = `${nodeId}_${pinId}`;
        const element = pinRefs[id];

        if (element === undefined)
            return null;

        const rect = element!.getBoundingClientRect();
        const canvasRect = canvasRef.current?.getBoundingClientRect() ?? {left: 0, top: 0};

        return { x: rect.left - canvasRect.left + rect.width / 2, y: rect.top - canvasRect.top + rect.height / 2};
    }, []);

    const getViewportPoint = useCallback((x: number, y: number): {x: number, y: number} => {

        const canvasRect = canvasRef.current?.getBoundingClientRect() ?? {x: 0, y: 0};

        return {x: x - canvasRect.x, y: y - canvasRect.y};//TODO: viewport
    }, [canvasRef]);

    return {
        nodeDefinitions,
        nodes,
        connections,
        viewport,
        addNode,
        removeNode,
        moveNode,
        registerPinRef,
        createEdge,
        removeEdge,
        getPinPosition,
        registerCanvas: setCanvasRef,
        moveViewport,
        zoomViewport,
        getViewportPoint
    };
}