import { MouseEvent, useCallback, useEffect, useRef, useState, WheelEvent } from "react"
import { Node } from "./Node";
import "./Node.css";
import { useNodeSystem } from "../../hooks/NodeSystem";
import { NodeFieldBackground } from "./NodeFieldBackground";
import { NodeEdge } from "./NodeEdge";
import { NodeConnectionState, NodeConnectionStateDefault } from "../../api/nodes/NodeConnectionState";
import { TemporaryNodeEdge } from "./TemporaryNodeEdge";
import { useDragSystem } from "../../hooks/NodeDragSystem";

export const NodeField = () => {

    const canvasRef = useRef<HTMLDivElement | null>(null);

    const {
        nodeDefinitions,
        nodes,
        connections,
        addNode,
        removeNode,
        moveNode,
        createEdge,
        removeEdge } = useNodeSystem();

    const {
        viewport,
        state: dragState,
        onMouseClick,
        onMouseDown,
        onMouseMove,
        onNodeDown,
        onScroll,
        onUnfocus,
        onPinClick,
        registerPin,
        getPinPosition,
        setCanvasRef: registerCanvas
    } = useDragSystem({createEdge, moveNode});


    const setCanvasRef = useCallback((el: HTMLDivElement | null) => {
        canvasRef.current = el;
        registerCanvas(el);
    }, [registerCanvas]);

    const onEdgeClick = useCallback((e: React.MouseEvent, id: string) => {
        removeEdge(id);
    }, [removeEdge]);

    return (
        <div className="overflow-hidden relative touch-none w-full h-full origin-top-left bg-[#e0e0e0] select-none"
            onMouseDown={onMouseDown}
            onClick={onMouseClick}
            onMouseMove={onMouseMove}
            onMouseUp={onUnfocus}
            onBlur={onUnfocus}
            onScroll={onScroll}
            onMouseLeave={onUnfocus}>

            <NodeFieldBackground viewport={viewport} />

            <div className="relative w-full h-full"
                ref={setCanvasRef}
                style={{ transform: `translate(${viewport.x}px, ${viewport.y}px) scale(${viewport.zoom})` }}>

                <svg className="absolute w-full h-full pointer-events-auto inset-0">
                    {connections.map((connection) => {

                        return (
                            <NodeEdge 
                                connection={connection}
                                onEdgeClick={onEdgeClick}
                                getPinPosition={getPinPosition} />
                        )
                    })}

                    {dragState.type === "connection"  &&
                        <TemporaryNodeEdge
                            sourceX={dragState.sourcePosition.x} 
                            sourceY={dragState.sourcePosition.y} 
                            targetX={dragState.targetPosition.x}
                            targetY={dragState.targetPosition.y}
                        />
                    }
                </svg>

                {nodes.map((node) => {

                    const definition = nodeDefinitions.find(d => d.id === node.definitionId);

                    if (definition === undefined)
                        return null;

                    return (
                        <div className="absolute" style={{ transform: `translate(${node.position.x}px, ${node.position.y}px)` }}>
                            <Node
                                key={node.id}
                                definition={definition}
                                instance={node}
                                headerMouseDownCallback={onNodeDown}
                                registerPinRef={registerPin}
                                pinClickCallback={onPinClick} />
                        </div>
                    )
                })}
            </div>
        </div>
    )
}