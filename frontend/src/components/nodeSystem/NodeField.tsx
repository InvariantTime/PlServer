import { MouseEvent, useCallback, useEffect, useRef, useState, WheelEvent } from "react"
import { Node } from "./Node";
import { NodePin, NodeInfo } from "../../api/nodes/NodeInfo";
import { CreateObjectType, ObjectTypeClass } from "../../api/nodes/ObjectType";
import "./Node.css";
import { NodeDefinition } from "../../api/nodes/NodeDefinition";
import { NodeEdgePresenter } from "./NodeEdgePresenter";
import { NodeConnection, NodeConnectionTypes, NodeEdgeDefinition } from "../../api/nodes/NodeConnection";
import { useNodeSystem } from "../../api/nodes/NodeSystem";

const nodeInfo: NodeInfo = {
    name: "Student builder",
    inputs: [{ name: "age", type: CreateObjectType(ObjectTypeClass.Number) }, { name: "name", type: CreateObjectType(ObjectTypeClass.String) },
    { name: "education's place", type: CreateObjectType(ObjectTypeClass.String) },
    { name: "education level", type: CreateObjectType(ObjectTypeClass.Enum) },
    { name: "course", type: CreateObjectType(ObjectTypeClass.Number) },
    { name: "department", type: CreateObjectType(ObjectTypeClass.String) }],
    outputs: [{ name: "student", type: CreateObjectType(ObjectTypeClass.Object) }],
    parameters: []
}

export const NodeField = () => {

    const {nodes, 
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
        zoomViewport} = useNodeSystem();


    const [isPanning, setIsPanning] = useState(false);
    const dragRef = useRef<{ startX: number, startY: number, prevX: number, prevY: number, id: number } | null>(null);
    const getNode = useCallback((id: number) => nodes.find(x => x.id === id), [nodes]);

    const onMouseMove = (e: MouseEvent<HTMLDivElement>) => {

       
    }

    const onMouseDown = (e: MouseEvent<HTMLDivElement>) => {

    }

    const onUnfocus = () => {
        setIsPanning(false);
        dragRef.current = null;
    }
    
    const onMouseWheel = (e: WheelEvent<HTMLDivElement>) => {
        const direction = e.deltaY > 0 ? 0.95 : 1.05;

        const newZoom = Math.min(viewport.zoom * direction, 2);
        zoomViewport(newZoom);
    }

    const onContextOpen = (e: MouseEvent<HTMLDivElement>) => {
        e.preventDefault();

        const bound = e.currentTarget.getBoundingClientRect();

        const relativeX = e.clientX - bound.left;
        const relativeY = e.clientY - bound.top;

        const x = (relativeX - viewport.x) / viewport.zoom;
        const y = (relativeY - viewport.y) / viewport.zoom;

        addNode({ x: x, y: y, info: nodeInfo, id: Date.now() });
    }


    const headerMouseDownCallback = (e: MouseEvent<HTMLElement>, id: number, x: number, y: number) => {
        e.stopPropagation();

        dragRef.current = {
            startX: e.clientX,
            startY: e.clientY,
            prevX: x,
            prevY: y,
            id: id,
        };
    }

    return (
        <div className="overflow-hidden relative touch-none w-full h-full origin-top-left bg-[#e0e0e0]"
            onMouseMove={onMouseMove}
            onMouseDown={onMouseDown}
            onWheel={onMouseWheel}
            onMouseUp={onUnfocus}
            onContextMenu={onContextOpen}
            onMouseLeave={onUnfocus}>
            <div className="node-field-background"
                style={{
                    '--vx': viewport.x,
                    '--vy': viewport.y,
                    backgroundSize: `${Math.max(4, Math.min(35, 20 * viewport.zoom))}px ${Math.max(4, Math.min(35, 20 * viewport.zoom))}px`,
                    opacity: viewport.zoom < 0.3 ? 0.3 : 1
                } as React.CSSProperties} />


            <div className="relative"
                style={{ transform: `translate(${viewport.x}px, ${viewport.y}px) scale(${viewport.zoom})` }}>

                <NodeEdgePresenter getNode={getNode} edges={edges}/>

                {nodes.map(node => {
                    return (
                        <div className="absolute"
                            style={{ left: node.x, top: node.y }}>
                            <Node info={node.info}
                                headerMouseDownCallback={(e) => headerMouseDownCallback(e, node.id, node.x, node.y)} />
                        </div>
                    )
                })}
            </div>
        </div>
    )
}