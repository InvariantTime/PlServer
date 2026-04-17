import React from "react";
import logo from "./logo.svg";
import { Lobby } from "./pages/lobby/Lobby";
import { SessionProvider } from "./components/sessions/SessionProvider";
import { Header } from "./components/header/Header";
import { ModalProvider } from "./components/modals/ModalProvider";
import { Session } from "./pages/session/Session";

const url = "/ws/lobby";

function App() {
  return (
    <ModalProvider>
      <SessionProvider url={url}>
        <div className="bg-slate-300 min-h-screen flex flex-col">
          <div className="h-[8%]">
            <Header />
          </div>

          <div className="flex flex-1">
            <Lobby/>
          </div>
        </div>
      </SessionProvider>
    </ModalProvider>
  );
}

export default App;
