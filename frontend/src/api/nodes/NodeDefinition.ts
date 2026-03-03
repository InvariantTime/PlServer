

export type NodeDefinition = {
  
    id: string,
    inputs: NodePin[],
    outputs: NodePin[]
}

export type NodePin = {
    id: string,
    name: string,
    type: "input" | "output"
}