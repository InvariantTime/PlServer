import { Play } from "lucide-react"
import { useCallback } from "react";
import ReactFlow, { addEdge, Background, Connection, Controls, Edge, MiniMap, useEdgesState, useNodesState } from "reactflow"
import 'reactflow/dist/style.css';

export const Session = () => {

    const [nodes, setNodes, onNodesChange] = useNodesState([
        {
            id: '1',
            position: { x: 100, y: 100 },
            data: { label: 'Первая нода' }
        },
        {
            id: '2',
            position: { x: 300, y: 200 },
            data: { label: 'Вторая нода' }
        }
    ]);

    const [edges, setEdges, onEdgesChange] = useEdgesState([]);

    const onConnect = useCallback(
        (params: Edge | Connection) => setEdges((eds) => addEdge(params, eds)),
        [setEdges]);


    return (
        <div className="min-h-full min-w-full p-4 flex gap-4">
            <div className="bg-slate-200 border-emerald-900 border-2 rounded-md h-full p-2 flex-[3]">
                <div className="h-full w-full">
                    <ReactFlow nodes={nodes}
                        onNodesChange={onNodesChange}
                        onConnect={onConnect}
                        edges={edges}
                        onEdgesChange={onEdgesChange}
                        fitView>

                        <Background />
                        <Controls />
                        <MiniMap />
                    </ReactFlow>
                </div>
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