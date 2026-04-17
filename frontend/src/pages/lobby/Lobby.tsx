
import { Plus, User } from "lucide-react";
import { useEffect, useState } from "react";
import { useSession } from "../../api/sessions/SessionContext";
import { SessionLobbyInfo } from "../../api/sessions/SessionLobbyInfo";
import { createSession, getSessionList } from "../../api/sessions/SessionQueries";
import { useDialog } from "../../api/modals/ModalPanelApi";
import { SessionCreationRequest } from "../../api/sessions/SessionCreationRequest";
import { SessionCreatePanel } from "../../components/sessions/SessionCreatePanel";


export const Lobby = () => {

  const [sessions, setSessions] = useState<SessionLobbyInfo[]>([]);
  const connection = useSession();
  const createSessionPanel = useDialog<SessionCreationRequest>(SessionCreatePanel);

  useEffect(() => {
    getSessionList().then((sessions) => {
      setSessions(sessions);
    });
    //setSessions([{name: "name", maxUserCount: 5, userCount: 2, hostName: "user", id: "id"}]);
  }, []);

  connection.connection.hub?.on("LobbyChangedAsync", () => {
    getSessionList().then((sessions) => {
      setSessions(sessions);
    });
  });

  /*useListen<SessionLobbyInfo[]>("OnSessionListChangedAsync", (sessions) =>
  {
    setSessions(sessions);
  });*/



  const onSessionCreateClick = async () => {

    const result = await createSessionPanel();
    await createSession(result);
  }

  return (
    <div className="mx-auto px-6 py-36 max-w-4xl w-full">
      <header>
        <div className="flex justify-center">
          <h1 className="text-4xl">
            Sessions
          </h1>
        </div>
      </header>

      <div className="space-y-4 mb-8 border-2 rounded-xl border-dashed border-emerald-900 mt-4 p-4">
        {sessions.map((session, index, array) => {

          const isFree = session.userCount < session.maxUserCount;

          return (
            <div className={`bg-slate-200 border-2 rounded-xl p-2 
             flex justify-between ${isFree ? "border-emerald-400 hover:bg-emerald-200": "border-red-300"}`}>
              <div className="pl-3">
                <h1 className="text-2xl">
                  {session.name}
                </h1>
                <h2 className="pl-8 text-lg text-slate-500">
                  {session.hostName}
                </h2>
              </div>

              <div className="mr-4 pt-2">
                <h2 className={isFree ? "" : "text-red-600"}>
                  <User/>
                  {session.userCount}/{session.maxUserCount}
                </h2>
              </div>
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
        <button className="w-full max-w-md bg-emerald-500 hover:bg-emerald-700 rounded-md p-3"
          onClick={onSessionCreateClick}>
          <div className="flex items-center justify-center gap-3">
            <div className="rounded-2xl bg-emerald-200 p-1">
              <Plus className="w-6 h-6" />
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