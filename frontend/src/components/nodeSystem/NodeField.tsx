import { MouseEvent, useCallback, useEffect, useRef, useState, WheelEvent } from "react"
import { Node } from "./Node";
import "./Node.css";
import { NodeDefinition } from "../../api/nodes/NodeDefinition";
import { useNodeSystem } from "../../api/nodes/NodeSystem";
import { NodeFieldBackground } from "./NodeFieldBackground";

export const NodeField = () => {

    const {nodes, 
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
        zoomViewport} = useNodeSystem();


    
    const onNodeMouseDown = useCallback((e: React.MouseEvent, id: string) => {

    }, []);
    


    return (
        <div className="overflow-hidden relative touch-none w-full h-full origin-top-left bg-[#e0e0e0]">
            <NodeFieldBackground viewport={viewport}/>

            <div className="relative"
                style={{ transform: `translate(${viewport.x}px, ${viewport.y}px) scale(${viewport.zoom})` }}>


                {nodes.map(node => {
                    return (
                        <div className="absolute">
                        </div>
                    )
                })}
            </div>
        </div>
    )
}