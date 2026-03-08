import { NodePinDefinition } from "../../api/nodes/NodeDefinition"


interface Props {
    pin: NodePinDefinition,
    registry: (element: HTMLDivElement) => void,
    pinClick?: () => void
}

export const NodePin = ({pin, registry}: Props) => {
    
    return (
        <div className="flex flex-row items-center gap-3">

            {pin.type === "output" && <h2 className="text-center font-bold">{pin.name}</h2>}

            <div className="w-3 h-3 rounded-full -m-[6px] bg-red-400 origin-center"
                ref={registry}/>

            {pin.type === "input" && <h2 className="text-center font-bold">{pin.name}</h2>}
        </div>
    )
}