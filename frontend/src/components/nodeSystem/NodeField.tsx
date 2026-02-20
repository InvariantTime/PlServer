import { MouseEvent, useState, WheelEvent } from "react"
import { Node } from "./Node";
import { NodeInfo } from "../../api/nodes/NodeInfo";
import { CreateObjectType, ObjectTypeClass } from "../../api/nodes/ObjectType";
import "./Node.css";
import { stringify } from "querystring";


type NodeDecloration = {
    info: NodeInfo,
    id: number
    x: number,
    y: number
}

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

    const [viewport, setViewport] = useState({ x: 0, y: 0, zoom: 1 });
    const [isPanning, setIsPanning] = useState(false);
    const [nodes, setNodes] = useState<NodeDecloration[]>([]);

    const onMouseMove = (e: MouseEvent<HTMLDivElement>) => {

        if (isPanning === true) {
            const x = viewport.x + e.movementX;
            const y = viewport.y + e.movementY;
            const zoom = viewport.zoom;

            setViewport({ x: x, y: y, zoom: zoom });
        }
    }

    const onMouseWheel = (e: WheelEvent<HTMLDivElement>) => {

    }

    const onMouseDown = (e: MouseEvent<HTMLDivElement>) => {

        if (e.button === 0) {
            setIsPanning(true);
        }
    }

    const onContextOpen = (e: MouseEvent<HTMLDivElement>) => {
        e.preventDefault();

        const bound = e.currentTarget.getBoundingClientRect();

        const relativeX = e.clientX - bound.left;
        const relativeY = e.clientY - bound.top;

        const x = (relativeX - viewport.x) / viewport.zoom;
        const y = (relativeY - viewport.y) / viewport.zoom;

        setNodes(prev => [...prev, { x: x, y: y, info: nodeInfo, id: 0 }]);
    }

    const onUnfocus = () => {
        setIsPanning(false);
    }

    return (
        <div className="node-field-background overflow-hidden relative touch-none w-full h-full origin-top-left"
            onMouseMove={onMouseMove}
            onMouseDown={onMouseDown}
            onWheel={onMouseWheel}
            onMouseUp={onUnfocus}
            onContextMenu={onContextOpen}
            onMouseLeave={onUnfocus}>
            <div className="relative"
                style={{ transform: `translate(${viewport.x}px, ${viewport.y}px) scale(${viewport.zoom})` }}>

                {nodes.map(node => {
                    return (
                        <div className="absolute"
                            style={{ left: node.x, top: node.y }}>
                            <Node info={node.info} />
                        </div>
                    )
                })}
            </div>
        </div>
    )
}