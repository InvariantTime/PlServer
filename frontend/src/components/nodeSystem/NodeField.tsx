import { MouseEvent, useRef, useState, WheelEvent } from "react"
import { Node } from "./Node";
import { NodeConnection, NodeInfo } from "../../api/nodes/NodeInfo";
import { CreateObjectType, ObjectTypeClass } from "../../api/nodes/ObjectType";
import "./Node.css";
import { stringify } from "querystring";
import { NodeEdge } from "./NodeEdge";
import { NodeDefinition } from "../../api/nodes/NodeDefinition";
import { NodeEdgePresenter } from "./NodeEdgePresenter";

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
    const [nodes, setNodes] = useState<NodeDefinition[]>([]);
    const [edges, setEdges] = useState<NodeConnection[]>([]);
    const dragRef = useRef<{ startX: number, startY: number, prevX: number, prevY: number, id: number } | null>(null);

    const dragNode = (e: MouseEvent<HTMLDivElement>) => {

        if (dragRef.current == null)
            return;

        const { id, startX, startY, prevX, prevY } = dragRef.current;

        const dx = (e.clientX - startX) / viewport.zoom;
        const dy = (e.clientY - startY) / viewport.zoom;

        setNodes(prev => {
            const dragged = prev.find(n => n.id == id);

            if (!dragged)
                return prev;

            return [...prev.filter(n => n.id != id), { ...dragged, x: dx + prevX, y: dy + prevY }];
        });
    }


    const onMouseMove = (e: MouseEvent<HTMLDivElement>) => {

        if (dragRef.current != null) {
            dragNode(e);
            return;
        }

        if (isPanning === true) {
            const x = viewport.x + e.movementX;
            const y = viewport.y + e.movementY;
            const zoom = viewport.zoom;

            setViewport({ x: x, y: y, zoom: zoom });
        }
    }

    const onMouseWheel = (e: WheelEvent<HTMLDivElement>) => {
        const direction = e.deltaY > 0 ? 0.95 : 1.05;

        const newViewport = Math.min(viewport.zoom * direction, 2);

        const x = viewport.x;
        const y = viewport.y;
        const zoom = newViewport;

        setViewport({ x: x, y: y, zoom: zoom });
    }

    const onMouseDown = (e: MouseEvent<HTMLDivElement>) => {

        if (e.button === 0) {
            setIsPanning(true);

            const edge = edges.at(0)!;

            const bound = e.currentTarget.getBoundingClientRect();

            const relativeX = e.clientX - bound.left;
            const relativeY = e.clientY - bound.top;

            const x = (relativeX - viewport.x) / viewport.zoom;
            const y = (relativeY - viewport.y) / viewport.zoom;

            setEdges(prev => [{ ...edge, endX: x, endY: y }])
        }
    }

    const onContextOpen = (e: MouseEvent<HTMLDivElement>) => {
        e.preventDefault();

        const bound = e.currentTarget.getBoundingClientRect();

        const relativeX = e.clientX - bound.left;
        const relativeY = e.clientY - bound.top;

        const x = (relativeX - viewport.x) / viewport.zoom;
        const y = (relativeY - viewport.y) / viewport.zoom;

        setNodes(prev => [...prev, { x: x, y: y, info: nodeInfo, id: Date.now() }]);
    }

    const onUnfocus = () => {
        setIsPanning(false);
        dragRef.current = null;
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

                <NodeEdgePresenter nodes={nodes} connections={edges}/>

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

            <svg>
                <path />
            </svg>
        </div>
    )
}