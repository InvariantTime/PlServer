import { ChevronDown, ChevronUp } from "lucide-react"
import React, { MouseEvent, useCallback, useState } from "react"
import { NodeInstance } from "../../api/nodes/NodeInstance"
import { NodeDefinition } from "../../api/nodes/NodeDefinition"
import { NodePin } from "./NodePin"

interface Props {
    instance: NodeInstance,
    definition: NodeDefinition,
    headerMouseDownCallback: (e: React.MouseEvent, id: string, x: number, y: number) => void,
    pinClickCallback: (e: React.MouseEvent, nodeId: string, pinId: string) => void,
    registerPinRef: (nodeId: string, pinId: string, element: HTMLDivElement) => void
}


export const Node = ({ instance, definition, headerMouseDownCallback, registerPinRef, pinClickCallback }: Props) => {

    return (
        <div className="w-48 flex flex-col shadow-xl border-[1px] rounded-md border-slate-400 select-none">

            <div className={`w-full rounded-t-md h-10 border-b-[1px] flex items-center px-2 gap-3 
                ${definition.isFinal == true ? "border-blue-700 bg-blue-600" : "border-red-700 bg-red-600"}`}
                onMouseDown={e => headerMouseDownCallback(e, instance.id, instance.position.x, instance.position.y)}>

                <div className="h-4 w-4 rounded-full bg-red-100 items-center justify-center flex 
                    hover:bg-red-200">
                    <ChevronUp />
                </div>

                <h1 className="text-white font-bold">{definition.name}</h1>
            </div>
            <div className="bg-slate-200 w-full rounded-b-md py-4">

                <div className="justify-end flex">
                    <div className="flex gap-2 flex-col">
                        {definition.outputs.map(output => {
                            return (
                                <NodePin pin={output}
                                    registry={(el) => registerPinRef(instance.id, output.id, el)}
                                    onClick={(el) => pinClickCallback(el, instance.id, output.id)} />
                            )
                        })}
                    </div>
                </div>

                {definition.id === "num" &&
                    <div className="flex gap-2 flex-col">
                        <div className="font-bold pl-1">number</div>
                        <div className="flex items-center justify-center">
                            <input className="text-center max-w-32 border-2 border-slate-200" type="number" />
                        </div>
                    </div>
                }


                <div className="justify-start flex gap-2 flex-col">
                    {definition.inputs.map(input => {
                        return (
                            <NodePin pin={input}
                                registry={(el) => registerPinRef(instance.id, input.id, el)}
                                onClick={(el) => pinClickCallback(el, instance.id, input.id)} />
                        )
                    })}
                </div>
            </div>
        </div>
    )
}