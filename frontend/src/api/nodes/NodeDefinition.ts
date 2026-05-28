

export type NodeDefinition = {
  
    id: string,
    name: string,
    isFinal: boolean,
    inputs: NodePinDefinition[],
    outputs: NodePinDefinition[]
}

export type NodePinDefinition = {
    id: string,
    name: string,
    type: "input" | "output"
}