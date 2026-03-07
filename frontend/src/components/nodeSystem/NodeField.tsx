import { MouseEvent, useCallback, useEffect, useRef, useState, WheelEvent } from "react"
import { Node } from "./Node";
import "./Node.css";
import { NodeDefinition } from "../../api/nodes/NodeDefinition";
import { useNodeSystem } from "../../api/nodes/NodeSystem";
import { NodeFieldBackground } from "./NodeFieldBackground";
import { NodeInstance } from "../../api/nodes/NodeInstance";
import { transform } from "typescript";
import { NodeEdge } from "./NodeEdge";
import { NodeConnection } from "../../api/nodes/NodeConnection";

export const NodeField = () => {

    const canvasRef = useRef<HTMLDivElement | null>(null);

    const {
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
        registerCanvas,
        moveViewport,
        zoomViewport } = useNodeSystem();


    const setCanvasRef = useCallback((el: HTMLDivElement | null) => {
        canvasRef.current = el;
        registerCanvas(el);
    }, [registerCanvas]);


    const onNodeMouseDown = useCallback((e: React.MouseEvent, id: string) => {

    }, []);

    const onMouseMove = useCallback((e: React.MouseEvent) => {
        moveNode(nodes.at(0)!.id, e.clientX, e.clientY);
    }, []);

    return (
        <div className="overflow-hidden relative touch-none w-full h-full origin-top-left bg-[#e0e0e0]" ref={setCanvasRef}
            onMouseMove={onMouseMove}>

            <NodeFieldBackground viewport={viewport} />

            <div className="relative w-full h-full"
                style={{ transform: `translate(${viewport.x}px, ${viewport.y}px) scale(${viewport.zoom})` }}>

                <svg className="absolute w-full h-full">
                    {connections.map((connection) => {

                        return (
                            <NodeEdge connection={connection} getPinPosition={getPinPosition} />
                        )
                    })}
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
                                headerMouseDownCallback={() => { }}
                                registerPinRef={registerPinRef} />
                        </div>
                    )
                })}
            </div>
        </div>
    )
}