export type NodeConnectionState = {
    source: {nodeId: string, pinId: string},
    sourcePosition: {x: number, y: number},
    targetPosition: {x: number, y: number},
    isConnecting: boolean
}

export const NodeConnectionStateDefault: NodeConnectionState = {
    source: {nodeId: "", pinId: ""},
    sourcePosition: {x: 0, y: 0},
    targetPosition: {x: 0, y: 0},
    isConnecting: false
}
