import { NodeConnection } from "./NodeConnection";
import { NodeInstance } from "./NodeInstance";

export type SyncSnapshot = SyncFullSnapshot | SyncDeltaSnapshot;

export type SyncFullSnapshot = {
    type: "full"
    version: number,
    connections: NodeConnection[],
    nodes: NodeInstance[]
}

export type SyncDeltaSnapshot = {
    type: "delta"
    version: number
}