import { NodeDefinition } from "./NodeDefinition"


export type NodeEdgeDefinition = {
    source: NodeConnection,
    target: NodeConnection
}


export type NodeConnection = {
    nodeId: number,
    pin: number,
    type: NodeConnectionTypes
}

export enum NodeConnectionTypes {
    Input,
    Output
}

export function CalculatePinPosition(node: NodeDefinition, pin: number, pinType: NodeConnectionTypes) {
    
    var offsetX = 0;
    var offsetY = 40;

    if (pinType === NodeConnectionTypes.Input)
        offsetY += (node.info.outputs.length + 1) * 24;


    if (pinType === NodeConnectionTypes.Output)
        offsetX += 190;


    offsetY += pin * 24;

    return {x: node.x + offsetX, y: node.y + offsetY};
}