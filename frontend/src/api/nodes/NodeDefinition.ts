

export type NodeDefinition = {
  
    id: string,
    inputs: NodePinDefinition[],
    outputs: NodePinDefinition[]
}

export type NodePinDefinition = {
    id: string,
    name: string,
    type: "input" | "output"
}