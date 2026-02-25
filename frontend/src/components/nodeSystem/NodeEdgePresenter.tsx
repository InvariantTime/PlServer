import { NodeConnection } from "../../api/nodes/NodeConnection";
import { NodeDefinition } from "../../api/nodes/NodeDefinition";
import { NodeEdge } from "./NodeEdge";


interface Props {
    connections: NodeConnection[];
    getNodePosition: (id: number) => ({x: number, y: number} | undefined)
}


export const NodeEdgePresenter = ({connections, getNodePosition}: Props) => {

    return (
        <svg className="absolute inset-0 overflow-visible">
            {connections.map((connection) => {
                const source = getNodePosition(connection.sourceId);
                const target = getNodePosition(connection.targetId);

                if (source === undefined || target === undefined)
                    return null;

                return (
                    <NodeEdge startX={source.x} startY={source.y} endX={target.x} endY={target.y} 
                        key={`${connection.sourceId}-${connection.targetId}`}/>
                )
            })}
        </svg>
    )
}