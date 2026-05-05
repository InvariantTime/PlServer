
export type NodeConnection = {
    source: NodeConnectionPair,
    target: NodeConnectionPair
}

export type NodeConnectionPair = {
    pinId: string,
    nodeId: string
}