import { ChevronDown, ChevronUp } from "lucide-react"
import { MouseEvent, useCallback, useState } from "react"
import { NodeInstance } from "../../api/nodes/NodeInstance"
import { NodeDefinition } from "../../api/nodes/NodeDefinition"
import { NodePin } from "./NodePin"

interface Props {
    instance: NodeInstance,
    definition: NodeDefinition,
    headerMouseDownCallback: (e: MouseEvent<HTMLElement>, id: string) => void
}


export const Node = ({ instance, definition, headerMouseDownCallback }: Props) => {

    return (
        <div className="w-48 flex flex-col shadow-xl border-[1px] rounded-md border-slate-400 select-none">
            <div className="bg-red-600 w-full rounded-t-md h-10 border-b-[1px] border-red-700 flex items-center px-2 gap-3"
                onMouseDown={e => headerMouseDownCallback(e, instance.id)}>

                <div className="h-4 w-4 rounded-full bg-red-100 items-center justify-center flex 
                    hover:bg-red-200">
                    <ChevronUp />
                </div>

                <h1 className="text-white font-bold">{instance.name}</h1>
            </div>
            <div className="bg-slate-200 w-full rounded-b-md py-4">

                <div className="justify-end flex">
                    <div className="flex gap-2 flex-col">
                        {definition.outputs.map(output => {
                            return (
                                <NodePin/>
                            )
                        })}
                    </div>
                </div>

                <div className="justify-start flex gap-2 flex-col">
                    {definition.inputs.map(input => {
                        return (
                            <NodePin/>
                        )
                    })}
                </div>
            </div>
        </div>
    )
}