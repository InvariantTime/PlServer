import { MouseEvent, useCallback, useRef, useState } from "react"
import { NodeDefinition } from "./NodeDefinition";
import { NodeViewport } from "../../components/nodeSystem/NodeViewport";
import { NodeConnection } from "./NodeConnection";


export const useNodeSystem = () => {

    const [nodes, setNodes] = useState<NodeDefinition[]>([]);
    const [connections, setConnections] = useState<NodeConnection[]>([]);
    const [viewport, setViewport] = useState<NodeViewport>({x: 0, y: 0, zoom: 1});


    const pinRefs = useRef<Record<string, HTMLDivElement | null>>({});
    const canvasRef = useRef<HTMLDivElement>(null);

    const addNode = useCallback((node: NodeDefinition) => {

    }, []);

    const removeNode = useCallback((nodeId: string) => {

    }, []);

    const registerPinRef = useCallback((nodeId: string, pinId: string, element: HTMLDivElement) => {
        const id = `${nodeId}_${pinId}`
        pinRefs.current[id] = element;
    }, []);

    const createEdge = useCallback(() => {

    }, []);

    const removeEdge = useCallback(() => {

    }, []);

    const setCanvasRef = useCallback((canvas: HTMLDivElement) => {
        canvasRef.current = canvas;
    }, []);

    const moveViewport = useCallback((xOffset: number, yOffset: number) => {
        setViewport(prev => {
            return {x: prev.x + xOffset, y: prev.y + yOffset, zoom: prev.zoom};
        });
    }, []);

    const zoomViewport = useCallback((zoom: number) => {
        setViewport(prev => {
            return {x: prev.x, y: prev.y, zoom: zoom};
        });
    }, []);

    const getPinPosition = useCallback((nodeId: string, pinId: string): {x: number, y: number} | null => {

        const id = `${nodeId}_${pinId}`;
        const element = pinRefs.current[id];

        if (element === undefined)
            return null;

        const rect = element!.getBoundingClientRect();
        //const canvasRect = canvasRef.current?.getBoundingClientRect();


        return {x: rect.left, y: rect.top};
    }, []);

    return {
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