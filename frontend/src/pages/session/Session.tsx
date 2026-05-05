import { Play } from "lucide-react"
import { NodeField } from "../../components/nodeSystem/NodeField";
import { useSession } from "../../api/signalR/SessionConnection";
import { useNotify } from "../../api/notifying/Notification";
import { useSearchParams } from "react-router-dom";
import { NodeGraphAdapter } from "../../api/nodes/NodeGraphAdapter";

const sessionUrl = "ws/sessions?sessionId=";

export const Session = () => {

    const [query, _] = useSearchParams();
    const url = sessionUrl + query.get("sessionId");

    const notify = useNotify();
    const {adapter} = useSession({url: url, onMessage: notify });

    return (
        <div className="min-h-full min-w-full p-4 flex gap-4">
            <div className="bg-slate-200 border-emerald-900 border-2 rounded-md min-h-full p-2 flex flex-[3]">
                <NodeField adapter={adapter}/>
            </div>

            <div className="bg-slate-200 border-emerald-900 border-2 rounded-md flex-[1] min-h-full">
                <div className="h-12 p-2 shadow-md">
                    <Play size={34} strokeWidth={2} color="#2cc352" />
                </div>

                <div className="">
                </div>
            </div>
        </div>
    )
}