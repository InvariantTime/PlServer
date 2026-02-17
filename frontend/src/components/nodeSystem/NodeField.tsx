import { MouseEvent, useState, WheelEvent } from "react"
import { Node } from "./Node";
import { NodeInfo } from "../../api/nodes/NodeInfo";
import { CreateObjectType, ObjectTypeClass } from "../../api/nodes/ObjectType";


type SimpleNode = {
    color: string,
    x: number,
    y: number
}

const nodeInfo: NodeInfo = {
    name: "Student builder",
    inputs: [{name: "age", type: CreateObjectType(ObjectTypeClass.Number)}, {name: "name", type: CreateObjectType(ObjectTypeClass.String)}, 
        {name: "education level", type: CreateObjectType(ObjectTypeClass.Enum)}],
    outputs: [{name: "student", type: CreateObjectType(ObjectTypeClass.Object)}],
    parameters: []
}

export const NodeField = () => {

    const [viewport, setViewport] = useState({ x: 0, y: 0, zoom: 1 });
    const [isPanning, setIsPanning] = useState(false);
    const [nodes, setNodes] = useState<SimpleNode[]>([{x: 0, y: 0, color: ""}]);

    const onMouseMove = (e: MouseEvent<HTMLDivElement>) => {

        if (isPanning === true) {
            const x = viewport.x + e.movementX;
            const y = viewport.y + e.movementY;
            const zoom = viewport.zoom;

            setViewport({ x: x, y: y, zoom: zoom });
        }
    }

    const onMouseWheel = (e: WheelEvent<HTMLDivElement>) => {
        const zoomSpeed = 0.1;
        const newZoom = e.deltaY > 0 
            ? viewport.zoom * (1 - zoomSpeed)
            : viewport.zoom * (1 + zoomSpeed);

        const mouseX = e.clientX;
        const mouseY = e.clientY;
        const newX = mouseX - (mouseX - viewport.x) * (newZoom / viewport.zoom);
        const newY = mouseY - (mouseY - viewport.y) * (newZoom / viewport.zoom);
    
        setViewport({ x: newX, y: newY, zoom: newZoom });
    }

    const onMouseDown = (e: MouseEvent<HTMLDivElement>) => {

        if (e.button === 0) {
            setIsPanning(true);
        }
    }

    const onUnfocus = () => {
        setIsPanning(false);
    }

    return (
        <div className="bg-slate-300 overflow-hidden relative touch-none w-full h-full origin-top-left"
            onMouseMove={onMouseMove}
            onMouseDown={onMouseDown}
            onWheel={onMouseWheel}
            onMouseUp={onUnfocus}
            onMouseLeave={onUnfocus}>
            <div className="relative"
                style={{ transform: `translate(${viewport.x}px, ${viewport.y}px) scale(${viewport.zoom})` }}>

                {nodes.map(node => {
                    return (
                            <Node info={nodeInfo}/>
                    )
                })}
            </div>
        </div>
    )
}