import { ObjectType } from "./ObjectType"

export type NodeInfo = {
    name: string,
    inputs: NodeConnection[],
    outputs: NodeConnection[],
    parameters: NodeParameter[]
}

export type NodeConnection = {
    name: string,
    type: ObjectType
}

export type NodeParameter = {

}