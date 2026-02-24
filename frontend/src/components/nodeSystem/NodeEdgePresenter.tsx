import { NodeDefinition } from "../../api/nodes/NodeDefinition";
import { NodeConnection } from "../../api/nodes/NodeInfo";


interface Props {
    connections: NodeConnection[];
    nodes: NodeDefinition[]
}


export const NodeEdgePresenter = ({connections, nodes}: Props) => {

    return (
        <svg className="absolute inset-0 overflow-visible">
        </svg>
    )
}