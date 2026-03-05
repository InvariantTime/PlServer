import { MouseEvent, useCallback, useEffect, useRef, useState, WheelEvent } from "react"
import { Node } from "./Node";
import "./Node.css";
import { NodeDefinition } from "../../api/nodes/NodeDefinition";
import { useNodeSystem } from "../../api/nodes/NodeSystem";
import { NodeFieldBackground } from "./NodeFieldBackground";
import { NodeInstance } from "../../api/nodes/NodeInstance";
import { transform } from "typescript";
import { NodeEdge } from "./NodeEdge";

const definition : NodeDefinition = {
    id: "AFfdsfdsfdsf",
    inputs: [{id: "dsadas", name: "pin 1", type: "input"}, {id: "dsdas", name: "pin 2", type: "input"}],
    outputs: [{id: "vvv", name: "output", type: "output"}]
}

const instance : NodeInstance = {
    collapsed: false,
    definitionId: "AFfdsfdsfdsf",
    id: "vncvmdfdsfdsfnm",
    name: "My node",
    position: {x: 200, y: 200},
    values: []
}

const instance2 : NodeInstance = {
    collapsed: false,
    definitionId: "AFfdsfdsfdsf",
    id: "cccmmmvdvd",
    name: "My node",
    position: {x: 400, y: 400},
    values: []
}


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

    const sourcePin = getPinPosition(instance.id, definition.outputs[0].id);
    const targetPin = getPinPosition(instance2.id, definition.inputs[1].id);

    return (
        <div className="overflow-hidden relative touch-none w-full h-full origin-top-left bg-[#e0e0e0]" ref={setCanvasRef}>
            <NodeFieldBackground viewport={viewport}/>

            <div className="relative"
                style={{ transform: `translate(${viewport.x}px, ${viewport.y}px) scale(${viewport.zoom})` }}>

                <div>
                    <NodeEdge startX={sourcePin?.x ?? 0} startY={sourcePin?.y ?? 0} endX={targetPin?.x ?? 0} endY={targetPin?.y ?? 0}/>
                </div>


                <div className="absolute" style={{transform: `translate(${instance.position.x}px, ${instance.position.y}px)`}}>
                    <Node 
                        key={instance.id}
                        definition={definition} 
                        instance={instance} 
                        headerMouseDownCallback={() => {}} 
                        registerPinRef={registerPinRef}/>
                </div>

                <div className="absolute" style={{transform: `translate(${instance2.position.x}px, ${instance2.position.y}px)`}}>
                    <Node 
                        key={instance.id}
                        definition={definition} 
                        instance={instance} 
                        headerMouseDownCallback={() => {}} 
                        registerPinRef={registerPinRef}/>
                </div>
            </div>
        </div>
    )
}