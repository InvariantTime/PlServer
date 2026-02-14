import { Play } from "lucide-react"

export const Session = () => { 

    return (
        <div className="min-h-full min-w-full p-4 flex gap-4">
            <div className="bg-slate-200 border-emerald-900 border-2 rounded-md min-h-full p-2 flex-[3]">
                
            </div>

            <div className="bg-slate-200 border-emerald-900 border-2 rounded-md flex-[1] min-h-full pl-1 pr-1">
                <div className="h-12 p-2 border-b-2 border-emerald-900">
                    <Play size={34} strokeWidth={2} color="#2cc352" />
                </div>

                <div className="">
                </div>
            </div>
        </div>
    )
}