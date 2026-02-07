import { HubConnectionState } from "@microsoft/signalr";
import { useSession } from "../api/sessions/SessionContext";
import { Check, Ellipsis, X } from "lucide-react";
import { stat } from "fs";

export const Header = () => {

    const session = useSession();

    return (
        <div className="w-full h-16 bg-slate-100 shadow-lg flex justify-between">
            <div className="items-center flex pl-4 font-bold text-lg">
                PlServer
            </div>

            <div className="items-center flex pr-6">
                <StatusBar state={session.connection.state}/>
            </div>
        </div>
    );
}

interface StatusBarProps {
    state: HubConnectionState
}

const StatusBar = ({state}: StatusBarProps) => {

    const getIcon = () => {

        if (state === HubConnectionState.Connected)
            return <Check size={24} color="#118c08" strokeWidth={3} />;

        if (state === HubConnectionState.Disconnected)
            return <X size={24} color="#aa0e0e" strokeWidth={3} />;

        return <Ellipsis size={24} color="#dbcb1f" strokeWidth={3} />;
    }

    return (
        <div className="flex-row flex items-center gap-2">
            {getIcon()}

            <div className="text-md font-bold items-center flex">
                {state}
            </div>
        </div>
    );
}