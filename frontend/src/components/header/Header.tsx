import { HubConnectionState } from "@microsoft/signalr";
import { useSession } from "../../api/sessions/SessionContext";
import { Check, Ellipsis, X } from "lucide-react";
import { useEffect, useState } from "react";
import "./Header.css";

export const Header = () => {

    const session = useSession();

    return (
        <div className="w-full h-16 bg-slate-100 shadow-lg flex justify-between">
            <div className="items-center flex pl-4 font-bold text-lg">
                PlServer
            </div>

            <div className="items-center flex pr-6 z-50">
                <StatusBar state={session.connection.state} />
            </div>
        </div>
    );
}

interface StatusBarProps {
    state: HubConnectionState
}

const StatusBar = ({ state }: StatusBarProps) => {

    const [phase, setPhase] = useState<'idle' | 'jump' | 'done'>('idle');

    useEffect(() => {

        if (state === HubConnectionState.Connected || state === HubConnectionState.Disconnected) {
            setPhase('jump');
            setTimeout(() => setPhase('done'), 2000);
        }
        else
        {
            setPhase('idle');
        }

    }, [state])


    const getIcon = () => {

        if (state === HubConnectionState.Connected)
            return <Check size={24} color="#118c08" strokeWidth={3} />;

        if (state === HubConnectionState.Disconnected)
            return <X size={24} color="#aa0e0e" strokeWidth={3} />;

        return <Ellipsis size={24} color="#dbcb1f" strokeWidth={3} />;
    }

    return (

        <div className={`flex items-center justify-center ${phase !== 'done' && 'gap-2'} border-2
             border-emerald-900 border-dashed rounded-full p-1`}>

            <div className="flex items-center">
                {getIcon()}
            </div>

            <div className={`overflow-hidden transition-all duration-500 ease-out
                ${phase === 'done' ? 'max-w-0' : 'max-w-[200px]'}`}>

                <div className={`text-md font-bold items-center flex mr-2
                    ${phase === 'jump' && 'jump-animation'}`}>
                    {state}
                </div>
            </div>
        </div>
    );
}