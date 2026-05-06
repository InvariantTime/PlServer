import { NodeConnection } from "./NodeConnection";
import { NodeDefinition } from "./NodeDefinition";
import { NodeInstance } from "./NodeInstance";

export interface NodeGraphAdapter {
    nodes: NodeInstance[],
    connections: NodeConnection[],
    definitions: NodeDefinition[],
    addNode: () => void,
    addConnection: () => void,
    removeNode: (id: string) => void,
    removeConnection: (targetNode: string, targetPin: string) => void
}

export type NodeGraphCommand = 
AddNodeCommand
| RemoveNodeCommand
| AddConnectionCommand
| RemoveConnectionCommand;

export type AddNodeCommand = {
    type: "add_node",
    position: {x: number, y: number},
    definition: string
}

export type RemoveNodeCommand = {
    type: "remove_node",
    nodeId: string
}

export type AddConnectionCommand = {
    type: "add_connection",
    connection: {target: {nodeId: string, pinId: string}, source: {nodeId: string, pinId: string}}
}

export type RemoveConnectionCommand = {
    type: "removeConnection",
    target: {nodeId: string, pinId: string}
}
