import { useCallback, useState } from "react";
import { NodeViewport } from "../components/nodeSystem/NodeViewport";

export type DragState = NodeDragState | ConnectionDragState | ViewportDragState | EmptyDrag;

export type EmptyDrag = {
    type: "none";
}

export type NodeDragState = {
    type: "node";
}

export type ConnectionDragState = {
    type: "connection";
    source: {nodeId: string, pinId: string},
    sourcePosition: {x: number, y: number},
    targetPosition: {x: number, y: number}
}

export type ViewportDragState = {
    type: "viewport";
}


interface Props {
    viewport: NodeViewport, 
    getPinPosition: (nodeId: string, pinId: string) => ({x: number, y: number} | null),
    getViewportPoint: (x: number, y: number) => {x: number, y: number},
    createEdge: (source: {nodeId: string, pinId: string}, target: {nodeId: string, pinId: string}) => void
}


export const useDragSystem = ({viewport, getPinPosition, getViewportPoint, createEdge}: Props) => {

    const [state, setState] = useState<DragState>({type: "none"});

    const onMouseDown = useCallback((e: React.MouseEvent) => {

        console.debug("click");
        if (state.type === "connection") {
            setState({type: "none"});
        }

    }, [state.type]);

    const onNodeDown = useCallback((nodeId: string) => {

    }, [state.type]);

    const onPinClick = useCallback((e: React.MouseEvent, nodeId: string, pinId: string) => {

        if (state.type === "none") {

            const position = getPinPosition(nodeId, pinId);

            if (position === null)
                return;

            const cursor = getViewportPoint(e.clientX, e.clientY);
            setState({type: "connection", source: {nodeId, pinId}, sourcePosition: position, targetPosition: cursor});
        }
        else if (state.type === "connection") {

            if (state.source.nodeId === nodeId && state.source.pinId === pinId)
                return;

            createEdge(state.source, { nodeId: nodeId, pinId: pinId});
            setState({type:"none"});

        }

        e.stopPropagation();

    }, [state.type, getViewportPoint]);

    const onMouseMove = useCallback((e: React.MouseEvent) => {

        if (state.type === "connection") {
            const cursor = getViewportPoint(e.clientX, e.clientY);

            setState(prev => {
                return {...prev, targetPosition: cursor};
            });
        }
        else if (state.type === "node") {

        }
        else if (state.type === "viewport") {

        }

    }, [state.type, getViewportPoint]);

    return {
        state,
        onMouseDown,
        onNodeDown,
        onPinClick,
        onMouseMove
    }
}