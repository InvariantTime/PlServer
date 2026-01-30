import { Plus } from "lucide-react";
import { useState } from "react";

export const Lobby = () => {
  
  const sessions : string[] = ["Session1", "Session2"]

  return (
    <div className="relative max-w-4xl mx-auto px-6 py-12">
      <header>
        <div className="flex justify-center">
            <h1 className="text-4xl">
              Sessions
            </h1>
        </div>
      </header>

      <div className="space-y-4 mb-8 border-2 rounded-xl border-dashed border-emerald-900 mt-4 p-4">
        {sessions.map((session, index, array) => {
          return (
            <div className="bg-slate-200 border-emerald-400 border-2 rounded-xl p-6 hover:bg-emerald-200">
              <h1>
                {session}
              </h1>
            </div>
          )
        })}

        {sessions.length === 0 && (
            <div className="text-center p-20">
              <p className="text-lg">No active sessions found</p>
              <p className="text-sm mt-2">Create a new one to get started</p>
            </div>
        )}
      </div>

      <div className="justify-center flex">
        <button className="w-full max-w-md bg-emerald-500 hover:bg-emerald-700 rounded-md p-3">
          <div className="flex items-center justify-center gap-3">
            <div className="rounded-2xl bg-emerald-200 p-1">
              <Plus className="w-6 h-6"/>
            </div>
            <span className="text-lg text-emerald-100 font-bold">
              Create new session
            </span>
          </div>
        </button>
      </div>
    </div>
  );
}