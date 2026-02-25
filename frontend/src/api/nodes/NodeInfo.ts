import { ObjectType } from "./ObjectType"

export type NodeInfo = {
    name: string,
    inputs: NodePin[],
    outputs: NodePin[],
    parameters: NodeParameter[]
}

export type NodePin = {
    name: string,
    type: ObjectType
}

export type NodeParameter = {

}