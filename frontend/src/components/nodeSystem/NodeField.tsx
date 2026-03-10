import { MouseEvent, useCallback, useEffect, useRef, useState, WheelEvent } from "react"
import { Node } from "./Node";
import "./Node.css";
import { useNodeSystem } from "../../api/nodes/NodeSystem";
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
        zoomViewport } = useNodeSystem();


    const setCanvasRef = useCallback((el: HTMLDivElement | null) => {
        canvasRef.current = el;
        registerCanvas(el);
    }, [registerCanvas]);


    const onNodeMouseDown = useCallback((e: React.MouseEvent, id: string) => {

    }, []);

    const onMouseMove = useCallback((e: React.MouseEvent) => {

        if (connectionState.isConnecting === true) {

            console.debug("move connection");
            const cursor = {x: e.clientX, y: e.clientY};

            setConnectionState(prev => {
                return {...prev, targetPosition: cursor};
            });
        }

    }, [connectionState]);

    const onPinClick = useCallback((e: React.MouseEvent, nodeId: string, pinId: string) => {

        console.debug("on pin click");
        if (connectionState.isConnecting === false) {

            const pos = getPinPosition(nodeId, pinId);
            const cursor = {x: e.clientX, y: e.clientY};

            if (pos === null)
                return;

            setConnectionState({source: {nodeId: nodeId, pinId: pinId}, sourcePosition: pos, targetPosition: cursor, isConnecting: true});
        }
        else {
            
        }
    }, []);

    return (
        <div className="overflow-hidden relative touch-none w-full h-full origin-top-left bg-[#e0e0e0]"
            onMouseMove={onMouseMove}>

            <h1 className="absolute text-xl">{connectionState.targetPosition.x}</h1>
             <h1 className="absolute text-xl my-20" key={connectionState.isConnecting.toString()}>{connectionState.isConnecting.toString()}</h1>
            <NodeFieldBackground viewport={viewport} />

            <div className="relative w-full h-full"
                ref={setCanvasRef}
                style={{ transform: `translate(${viewport.x}px, ${viewport.y}px) scale(${viewport.zoom})` }}>

                <svg className="absolute w-full h-full">
                    {connections.map((connection) => {

                        return (
                            <NodeEdge connection={connection} getPinPosition={getPinPosition} />
                        )
                    })}

                    {connectionState.isConnecting === true &&
                        <TemporaryNodeEdge
                            sourceX={connectionState.sourcePosition.x} 
                            sourceY={connectionState.sourcePosition.y} 
                            targetX={connectionState.targetPosition.x}
                            targetY={connectionState.targetPosition.y}/>}
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
                                registerPinRef={registerPinRef}
                                pinClickCallback={onPinClick} />
                        </div>
                    )
                })}
            </div>
        </div>
    )
}