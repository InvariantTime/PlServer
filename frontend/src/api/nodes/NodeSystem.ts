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
            position: { x: 400, y: 400 },
            values: []
        },
        {
            collapsed: false,
            definitionId: "AFfdsfdsfdsf",
            id: "vncvmdfdsfdsfnm",
            name: "My node",
            position: { x: 200, y: 200 },
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

    const registerPinRef = useCallback((nodeId: string, pinId: string, element: HTMLDivElement) => {
        const id = `${nodeId}_${pinId}`
        setPinRefs(prev => {
            prev[id] = element;
            return prev;
        });

    }, [pinRefs]);

    const createEdge = useCallback(() => {

    }, []);

    const removeEdge = useCallback(() => {

    }, []);

    const setCanvasRef = useCallback((canvas: HTMLDivElement) => {
        canvasRef.current = canvas;
    }, []);

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
        //const canvasRect = canvasRef.current?.getBoundingClientRect();

        return { x: rect.left, y: rect.top };
    }, []);

    return {
        nodeDefinitions,
        nodes,
        connections,
        viewport,
        addNode,
        removeNode,
        registerPinRef,
        createEdge,
        removeEdge,
        getPinPosition,
        setCanvasRef,
        moveViewport,
        zoomViewport
    };
}