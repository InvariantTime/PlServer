import { MouseEvent, useCallback, useEffect, useRef, useState, WheelEvent } from "react"
import { Node } from "./Node";
import "./Node.css";
import { useNodeSystem } from "../../hooks/NodeSystem";
import { NodeFieldBackground } from "./NodeFieldBackground";
import { NodeEdge } from "./NodeEdge";
import { NodeConnectionState, NodeConnectionStateDefault } from "../../api/nodes/NodeConnectionState";
import { TemporaryNodeEdge } from "./TemporaryNodeEdge";

export const NodeField = () => {

    const canvasRef = useRef<HTMLDivElement | null>(null);
    const [connectionState, setConnectionState] = useState<NodeConnectionState>(NodeConnectionStateDefault);

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
        zoomViewport,
        getViewportPoint } = useNodeSystem();


    const setCanvasRef = useCallback((el: HTMLDivElement | null) => {
        canvasRef.current = el;
        registerCanvas(el);
    }, [registerCanvas]);


    const onNodeMouseDown = useCallback((e: React.MouseEvent, id: string) => {
        e.stopPropagation();
    }, []);

    const onMouseDown = useCallback((e: React.MouseEvent) => {
        e.stopPropagation();

        if (connectionState.isConnecting === true) {
            setConnectionState(NodeConnectionStateDefault);
        }

    }, [connectionState.isConnecting]);

    const onMouseMove = useCallback((e: React.MouseEvent) => {

        if (connectionState.isConnecting === true) {
            const cursor = getViewportPoint(e.clientX, e.clientY);

            setConnectionState(prev => {
                return {...prev, targetPosition: cursor};
            });
        }

    }, [connectionState.isConnecting, getViewportPoint]);

    const onPinClick = useCallback((e: React.MouseEvent, nodeId: string, pinId: string) => {

        if (connectionState.isConnecting === false) {

            const pos = getPinPosition(nodeId, pinId);
            const cursor = getViewportPoint(e.clientX, e.clientY);

            if (pos === null)
                return;

            setConnectionState({source: {nodeId: nodeId, pinId: pinId}, sourcePosition: pos, targetPosition: cursor, isConnecting: true});
        }
        else {
            const source = connectionState.source;

            if (source.nodeId === nodeId && source.pinId === pinId) {
                setConnectionState(NodeConnectionStateDefault);
                return;
            }

            createEdge(source, {nodeId: nodeId, pinId: pinId});
            setConnectionState(NodeConnectionStateDefault);
        }
    }, [connectionState.isConnecting, getViewportPoint]);

    const onEdgeClick = useCallback((e: React.MouseEvent, id: string) => {
        removeEdge(id);
    }, [removeEdge]);

    return (
        <div className="overflow-hidden relative touch-none w-full h-full origin-top-left bg-[#e0e0e0] select-none"
            onClick={onMouseDown}
            onMouseMove={onMouseMove}>

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

                    {connectionState.isConnecting === true &&
                        <TemporaryNodeEdge
                            sourceX={connectionState.sourcePosition.x} 
                            sourceY={connectionState.sourcePosition.y} 
                            targetX={connectionState.targetPosition.x}
                            targetY={connectionState.targetPosition.y}
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
                                headerMouseDownCallback={onNodeMouseDown}
                                registerPinRef={registerPinRef}
                                pinClickCallback={onPinClick} />
                        </div>
                    )
                })}
            </div>
        </div>
    )
}