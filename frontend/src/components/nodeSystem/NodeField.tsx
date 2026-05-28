import { MouseEvent, useCallback, useEffect, useRef, useState, WheelEvent } from "react"
import { Node } from "./Node";
import "./Node.css";
import { useNodeSystem } from "../../hooks/NodeSystem";
import { NodeFieldBackground } from "./NodeFieldBackground";
import { NodeEdge } from "./NodeEdge";
import { TemporaryNodeEdge } from "./TemporaryNodeEdge";
import { useDragSystem } from "../../hooks/NodeDragSystem";
import { NodeGraphAdapter } from "../../api/nodes/NodeGraphAdapter";
import { NodeDefinition } from "../../api/nodes/NodeDefinition";

interface Props {
    adapter: NodeGraphAdapter
}

interface ContextProps {
    definitions: NodeDefinition[],
    onClick: (position: {x: number, y: number}, definition: NodeDefinition) => void,
    position: {x: number, y: number}
}

export const NodeContext = ({ definitions, position, onClick }: ContextProps) => {

    const onChoose = useCallback((e: MouseEvent, def: NodeDefinition) => {
        e.stopPropagation();
        onClick(position, def);
    }, [definitions, position, onClick]);

    return (
        <menu className="border-emerald-300 border-2 rounded-lg flex flex-col items-center justify-center p-1 bg-slate-200">
            {definitions.map(def => {
                return (
                    <div className="bg-slate-200 w-full text-center p-2 px-4 font-medium hover:bg-slate-300"
                        onClick={(e) => onChoose(e, def)}>
                        {def.name}
                    </div>
                )
            })}
        </menu>
    )
}

export const NodeField = ({ adapter }: Props) => {

    const canvasRef = useRef<HTMLDivElement | null>(null);
    const [contextState, setContextState] = useState<{ x: number, y: number } | null>(null);

    const {
        nodeDefinitions,
        nodes,
        connections,
        addNode,
        removeNode,
        moveNode,
        createEdge,
        removeEdge,
        lockNode,
        unlockNode } = useNodeSystem(adapter);


    const onFieldClick = (e: MouseEvent, x: number, y: number) => {
        setContextState(null);
    }

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
    } = useDragSystem({ createEdge, moveNode, onFieldClick, unlockNode, lockNode });

    const onNodeClick = (e: React.MouseEvent, nodeId: string, x: number, y: number) => {
    }

    const setCanvasRef = useCallback((el: HTMLDivElement | null) => {
        canvasRef.current = el;
        registerCanvas(el);
    }, [registerCanvas]);

    const onEdgeClick = useCallback((e: React.MouseEvent, id: string) => {
        removeEdge(id);
    }, [removeEdge]);

    const onContext = useCallback((e: MouseEvent) => {
        e.preventDefault();
        setContextState({ x: e.clientX, y: e.clientY });
    }, [adapter]);

    const onContextClick = useCallback((position: {x: number, y: number}, def: NodeDefinition) => {
        adapter.handleCommand({
            type: "add_node",
            definition: def.id,
            position: position
        });

        console.debug(def.name);
        setContextState(null);
    }, [adapter])

    return (
        <div className="overflow-hidden relative touch-none w-full h-full origin-top-left bg-[#e0e0e0] select-none"
            onMouseDown={onMouseDown}
            onClick={onMouseClick}
            onMouseMove={onMouseMove}
            onMouseUp={onUnfocus}
            onBlur={onUnfocus}
            onScroll={onScroll}
            onMouseLeave={onUnfocus}
            onContextMenu={onContext}>

            <NodeFieldBackground viewport={viewport} />

            <div className="relative w-full h-full"
                ref={setCanvasRef}
                style={{ transform: `translate(${viewport.x}px, ${viewport.y}px) scale(${viewport.zoom})` }}>

                <svg className="absolute w-full h-full pointer-events-auto inset-0">
                    {connections.values().map((connection) => {

                        return (
                            <NodeEdge
                                connection={connection}
                                id={`${connection.target.nodeId}_${connection.target.pinId}`}
                                onEdgeClick={onEdgeClick}
                                getPinPosition={getPinPosition} />
                        )
                    })}

                    {dragState.type === "connection" &&
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
                        <div className="absolute" style={{ transform: `translate(${node.position.x}px, ${node.position.y}px)` }}
                            onClick={e => onNodeClick(e, node.id, node.position.x, node.position.y)}>
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

            {contextState !== null &&
                <div className="fixed"
                    style={{
                        left: contextState.x,
                        top: contextState.y
                    }}>
                    <NodeContext definitions={adapter.definitions} 
                        onClick={onContextClick}
                        position={{x: contextState.x, y: contextState.y}}/>
                </div>
            }
        </div>
    )
}