import { CalculatePinPosition, NodeConnection, NodeEdgeDefinition } from "../../api/nodes/NodeConnection";
import { NodeDefinition } from "../../api/nodes/NodeDefinition";
import { NodeEdge } from "./NodeEdge";


interface Props {
    edges: NodeEdgeDefinition[];
    getNode: ((id: number) => NodeDefinition | undefined)
}


export const NodeEdgePresenter = ({edges, getNode}: Props) => {

    return (
        <svg className="absolute inset-0 overflow-visible">
            {edges.map((edge) => {
                const source = getNode(edge.source.nodeId);
                const target = getNode(edge.target.nodeId);

                if (source === undefined || target === undefined)
                    return null;

                const start = CalculatePinPosition(source, edge.source.pin, edge.source.type);
                const end = CalculatePinPosition(target, edge.target.pin, edge.target.type);

                return (
                    <NodeEdge startX={start.x} startY={start.y} endX={end.x} endY={end.y} 
                        key={`${source.x}-${source.y}-${target.x}-${target.y}`}/>
                )
            })}
        </svg>
    )
}