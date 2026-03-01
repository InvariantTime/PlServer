import { useCallback, useRef, useState } from "react"
import { NodeDefinition } from "./NodeDefinition";
import { NodeEdgeDefinition } from "./NodeConnection";
import { NodeViewport } from "../../components/nodeSystem/NodeViewport";


export const useNodeSystem = () => {

    const [nodes, setNodes] = useState<NodeDefinition[]>([]);
    const [edges, setEdges] = useState<NodeEdgeDefinition[]>([]);
    const [viewport, setViewport] = useState<NodeViewport>({x: 0, y: 0, zoom: 1});


    const pinRefs = useRef<Record<string, HTMLDivElement | null>>({});
    const canvasRef = useRef<HTMLDivElement>(null);

    const addNode = useCallback((node: NodeDefinition) => {

    }, []);

    const removeNode = useCallback((nodeId: string) => {

    }, []);

    const registerPinRef = useCallback(() => {

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

        const id = `${nodeId}-${pinId}`;
        const element = pinRefs.current[id];

        if (element === null)
            return null;

        const rect = element.getBoundingClientRect();
        return {x: rect.left, y: rect.top};
    }, []);

    const startDrag = useCallback(() => {

    }, [])

    const stopDrag = useCallback(() => {

    }, [])

    const onDrag = useCallback(() => {

    }, [])


    return {
        nodes,
        edges,
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