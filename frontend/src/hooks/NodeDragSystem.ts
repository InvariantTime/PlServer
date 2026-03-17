import { useCallback, useRef, useState } from "react";
import { NodeViewport } from "../components/nodeSystem/NodeViewport";

export type DragState = NodeDragState | ConnectionDragState | ViewportDragState | EmptyDrag;

export type EmptyDrag = {
    type: "none";
}

export type NodeDragState = {
    type: "node";
    nodeId: string,
    offset: { x: number, y: number }
}

export type ConnectionDragState = {
    type: "connection";
    source: { nodeId: string, pinId: string },
    sourcePosition: { x: number, y: number },
    targetPosition: { x: number, y: number }
}

export type ViewportDragState = {
    type: "viewport";
}


interface Props {
    createEdge: (source: { nodeId: string, pinId: string }, target: { nodeId: string, pinId: string }) => void,
    moveNode: (nodeId: string, x: number, y: number) => void
}


export const useDragSystem = ({ createEdge, moveNode }: Props) => {

    const [state, setState] = useState<DragState>({ type: "none" });
    const [viewport, setViewport] = useState<NodeViewport>({ x: 0, y: 0, zoom: 1 });
    const [pinRefs, setPinRefs] = useState<Record<string, HTMLDivElement>>({});

    const canvasRef = useRef<HTMLDivElement | null>(null);

    const setCanvasRef = useCallback((element: HTMLDivElement | null) => {
        canvasRef.current = element;
    }, [canvasRef]);

    const registerPin = useCallback((nodeId: string, pinId: string, element: HTMLDivElement) => {
        const id = `${nodeId}_${pinId}`
        setPinRefs(prev => {
            prev[id] = element;
            return prev;
        });

    }, [pinRefs]);

    const getViewportPoint = useCallback((x: number, y: number): { x: number, y: number } => {

        const canvasRect = canvasRef.current?.getBoundingClientRect() ?? { x: 0, y: 0 };
        return { x: x - canvasRect.x, y: y - canvasRect.y };//TODO: viewport

    }, [canvasRef, viewport]);

    const getPinPosition = useCallback((nodeId: string, pinId: string): { x: number, y: number } | null => {

        const id = `${nodeId}_${pinId}`;
        const element = pinRefs[id];

        if (element === undefined)
            return null;

        const rect = element!.getBoundingClientRect();
        const canvasRect = canvasRef.current?.getBoundingClientRect() ?? { left: 0, top: 0 };//TODO: remove pin ref logic from node system logic. Node system logic is api for act with backend

        return { x: rect.left - canvasRect.left + rect.width / 2, y: rect.top - canvasRect.top + rect.height / 2 };
    }, []);

    const onMouseClick = useCallback((e: React.MouseEvent) => {

        if (state.type === "connection") {
            setState({ type: "none" });
        }

    }, [state.type]);

    const onMouseDown = useCallback((e: React.MouseEvent) => {
        if (state.type === "none") {
            setState({ type: "viewport" });
        }
    }, [state.type]);

    const onNodeDown = useCallback((e: React.MouseEvent, nodeId: string, x: number, y: number) => {
        if (state.type === "none") {
            e.stopPropagation();

            const start = { x: e.clientX, y: e.clientY };
            setState({ type: "node", nodeId: nodeId, offset: { x: x - start.x, y: y - start.y } });
        }

    }, [state.type]);

    const onPinClick = useCallback((e: React.MouseEvent, nodeId: string, pinId: string) => {

        if (state.type === "none") {

            const position = getPinPosition(nodeId, pinId);

            if (position === null)
                return;

            const cursor = getViewportPoint(e.clientX, e.clientY);
            setState({ type: "connection", source: { nodeId, pinId }, sourcePosition: position, targetPosition: cursor });
        }
        else if (state.type === "connection") {

            if (state.source.nodeId === nodeId && state.source.pinId === pinId)
                return;

            createEdge(state.source, { nodeId: nodeId, pinId: pinId });
            setState({ type: "none" });

        }

        e.stopPropagation();

    }, [state.type, getViewportPoint]);

    const onMouseMove = useCallback((e: React.MouseEvent) => {

        if (state.type === "connection") {
            const cursor = getViewportPoint(e.clientX, e.clientY);

            setState(prev => {
                return { ...prev, targetPosition: cursor };
            });
        }
        else if (state.type === "node") {

            const x = e.clientX + state.offset.x;
            const y = e.clientY + state.offset.y;
            moveNode(state.nodeId, x, y);
        }
        else if (state.type === "viewport") {
            setViewport(prev => ({ ...prev, x: prev.x + e.movementX, y: prev.y + e.movementY }));
        }

    }, [state.type, getViewportPoint]);

    const onScroll = useCallback((e: React.UIEvent) => {
        console.debug("scroll");
        setViewport(prev => ({...prev, zoom: prev.zoom + e.currentTarget.scrollTop}));
    }, []);

    const onUnfocus = useCallback(() => {

        if (state.type === "viewport" || state.type === "node") {
            setState({ type: "none" });
        }

    }, [state.type]);

    return {
        state,
        viewport,
        setCanvasRef,
        registerPin,
        getPinPosition,

        onMouseDown,
        onMouseClick,
        onNodeDown,
        onPinClick,
        onMouseMove,
        onUnfocus,
        onScroll
    }
}