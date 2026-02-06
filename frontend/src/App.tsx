import React from 'react';
import logo from './logo.svg';
import { LobbyPage } from './pages/lobby/LobbyExample';
import { Lobby } from './pages/lobby/Lobby';
import { SessionProvider } from './components/sessions/SessionProvider';

const url = "/ws/sessions"

function App() {
  return (
    <SessionProvider url={url}>
      <div className='bg-slate-300 min-h-screen'>
          <Lobby/>
      </div>
    </SessionProvider>
  );
}

export default App;
