import { NodeConnection } from "./NodeConnection";

export enum NodeGraphEvents {
    None = 0,
    AddedNode = 1,
    RemovedNode = 2,
    AddedConnection = 3,
    RemovedConnection = 4,
    MovedNode = 5,
    ChangedParameter = 6
}

export type NodeGraphEvent = 
  NoneEvent
| NodeAddedEvent
| NodeRemovedEvent
| ConnectionAddedEvent
| ConnectionRemovedEvent
| NodeMovedEvent;

export type NodeGraphEventBase = {
    version: number,
    type: NodeGraphEvents
}

export type NoneEvent = NodeGraphEventBase & {
    type: NodeGraphEvents.None
}

export type NodeAddedEvent = NodeGraphEventBase & {
    type: NodeGraphEvents.AddedNode,
    nodeId: string
}

export type NodeRemovedEvent = NodeGraphEventBase & {
    type: NodeGraphEvents.RemovedNode,
    nodeId: string
}

export type ConnectionAddedEvent = NodeGraphEventBase & {
    type: NodeGraphEvents.AddedConnection,
    connection: NodeConnection
}

export type ConnectionRemovedEvent = NodeGraphEventBase & {
    type: NodeGraphEvents.RemovedConnection,
    target: { nodeId: string, pinId: string}
}

export type NodeMovedEvent = NodeGraphEventBase & {
    type: NodeGraphEvents.MovedNode,
    nodeId: string,
    position: {x: number, y: number}
}

export type ChangedParameterEvent = NodeGraphEventBase & {
    type: NodeGraphEvents.ChangedParameter
}