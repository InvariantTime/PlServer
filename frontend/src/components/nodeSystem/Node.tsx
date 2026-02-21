import { ChevronDown, ChevronUp } from "lucide-react"
import { NodeConnection, NodeInfo } from "../../api/nodes/NodeInfo"
import { ObjectTypeClass } from "../../api/nodes/ObjectType"
import { MouseEvent, useCallback, useState } from "react"

interface Props {
    info: NodeInfo,
    headerMouseDownCallback: (e: MouseEvent<HTMLElement>) => void
}


export const Node = ({ info, headerMouseDownCallback }: Props) => {

    const [collapsed, setCollapsed] = useState(false);

    const openCallback = useCallback(() => {
        setCollapsed(false);
    }, []);

    const closeCallback = useCallback(() => {
        setCollapsed(true);
    }, []);

    if (collapsed === true)
        return (
            <div className="w-48 flex flex-col shadow-xl border-[1px] rounded-md border-slate-400">
                <CollapsedNode info={info} openCallback={openCallback}/>
            </div>
        );


    return (
        <div className="w-48 flex flex-col shadow-xl border-[1px] rounded-md border-slate-400 select-none">
            <div className="bg-red-600 w-full rounded-t-md h-10 border-b-[1px] border-red-700 flex items-center px-2 gap-3"
                onMouseDown={headerMouseDownCallback}>

                <div className="h-4 w-4 rounded-full bg-red-100 items-center justify-center flex 
                    hover:bg-red-200"
                    onClick={closeCallback}>
                    <ChevronUp />
                </div>

                <h1 className="text-white font-bold">{info.name}</h1>
            </div>
            <div className="bg-slate-200 w-full rounded-b-md py-4">

                <div className="justify-end flex">
                    <div className="flex gap-2 flex-col">
                        {info.outputs.map(output => {
                            return (
                                <div className="flex flex-row items-center gap-3">
                                    <h2 className="text-center font-bold">{output.name}</h2>
                                    <div className="w-3 h-3 rounded-full -m-[6px]" 
                                        style={{backgroundColor: getTypeColor(output.type.class)}}/>
                                </div>
                            )
                        })}
                    </div>
                </div>

                <div className="justify-start flex gap-2 flex-col">
                    {info.inputs.map(input => {
                        return (
                            <div className="flex flex-row items-center gap-3">
                                <div className="w-3 h-3 rounded-full -m-[6px]" 
                                    style={{backgroundColor: getTypeColor(input.type.class)}}/>
                                <h2 className="text-center font-bold">{input.name}</h2>
                            </div>
                        )
                    })}
                </div>
            </div>
        </div>
    )
}



interface CollapsedNodeProps {
    info: NodeInfo,
    openCallback: () => void
}

export const CollapsedNode = ({info, openCallback}: CollapsedNodeProps) => {

    const onOpen = useCallback(() => {
        openCallback();
    }, []);


    return (
        <div className="bg-red-600 w-full rounded-md h-10 border-[1px] border-red-700 flex items-center px-2 gap-3">
            <div className="h-4 w-4 rounded-full bg-red-100 items-center justify-center flex
                hover:bg-red-200"
                onClick={onOpen}>
                <ChevronDown />
            </div>

            <h1 className="text-white font-bold">{info.name}</h1>
        </div>
    );
}



function getTypeColor(cl: ObjectTypeClass): string {

    if (cl === ObjectTypeClass.String)
        return "#d45313";

    if (cl === ObjectTypeClass.Number)
        return "#1db5a1";

    if (cl === ObjectTypeClass.Enum)
        return "#d4bd13";

    return "#b935db";
}

function getTypeActiveColor(cl: ObjectTypeClass): string {

    if (cl === ObjectTypeClass.String)
        return "#d45313";

    if (cl === ObjectTypeClass.Number)
        return "#1db5a1";

    if (cl === ObjectTypeClass.Enum)
        return "#d4bd13";

    return "#b935db";
}