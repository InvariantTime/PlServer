import { NodeDefinition } from "./NodeDefinition"

export type NodeInstance = {
    id: string,
    name: string,
    definitionId: string,
    collapsed: boolean,
    values: Record<string, any>,
    
    position: {
        x: number,
        y: number
    }
}