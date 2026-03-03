
export type NodeConnection = {
    id: string,

    source: {
        nodeId: string,
        pinId: string
    },

    target: {
        nodeId: string,
        pinId: string
    }
}