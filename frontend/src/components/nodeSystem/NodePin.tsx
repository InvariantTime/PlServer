import { NodePinDefinition } from "../../api/nodes/NodeDefinition"


interface Props {
    pin: NodePinDefinition,
    registry: () => void,
    pinClick?: () => void
}

export const NodePin = ({pin, registry}: Props) => {
    
    return (
        <div className="flex flex-row items-center gap-3">

            {pin.type === "output" && 
                <>
                    <h2 className="text-center font-bold">{pin.name}</h2>
                    <div className="w-3 h-3 rounded-full -m-[6px] bg-red-400"/>
                </>
            }

            {pin.type === "input" && 
                <>
                    <div className="w-3 h-3 rounded-full -m-[6px] bg-green-400"/>
                    <h2 className="text-center font-bold">{pin.name}</h2>
                </>
            }
        </div>
    )
}

/*

///OUTPUT
                                <div className="flex flex-row items-center gap-3">
                                    <h2 className="text-center font-bold">{output.name}</h2>
                                    <div className="w-3 h-3 rounded-full -m-[6px]" 
                                        style={{backgroundColor: getTypeColor(output.type.class)}}/>
                                </div>


///INPUT

                            <div className="flex flex-row items-center gap-3">
                                <div className="w-3 h-3 rounded-full -m-[6px]" 
                                    style={{backgroundColor: getTypeColor(input.type.class)}}/>
                                <h2 className="text-center font-bold">{input.name}</h2>
                            </div>


*/