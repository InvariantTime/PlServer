import React from "react";
import logo from "./logo.svg";
import { LobbyPage } from "./pages/lobby/LobbyExample";
import { Lobby } from "./pages/lobby/Lobby";
import { SessionProvider } from "./components/sessions/SessionProvider";
import { Header } from "./components/Header";
import { ModalProvider } from "./components/modals/ModalProvider";
import { Session } from "./pages/lobby/session/Session";

const url = "/ws/sessions";

function App() {
  return (
    <ModalProvider>
      <SessionProvider url={url}>
        <div className="bg-slate-300 min-h-screen">
          <div className="h-[8%]">
            <Header />
          </div>

          <div className="h-full">
            <Session/>
          </div>
        </div>
      </SessionProvider>
    </ModalProvider>
  );
}

export default App;
