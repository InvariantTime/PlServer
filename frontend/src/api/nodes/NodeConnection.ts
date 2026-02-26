import { NodeDefinition } from "./NodeDefinition"


export type NodeConnection = {
    sourceId: number,
    targetId: number,
    pin: number,
    type: NodeConnectionTypes
}

export enum NodeConnectionTypes {
    Input,
    Output
}

export function CalculatePinPosition(node: NodeDefinition, connection: NodeConnection) {
    
    return {x: node.x, y: node.y};
}