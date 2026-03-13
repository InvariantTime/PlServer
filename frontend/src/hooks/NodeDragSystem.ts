
export type DragState = NodeDragState | ConnectionDragState | ViewportDragState;

export type NodeDragState = {

}

export type ConnectionDragState = {
    source: {},
    sourcePosition: {x: number, y: number},
    targetPosition: {x: number, y: number}
}

export type ViewportDragState = {

}

export const useDragSystem = () => {

}