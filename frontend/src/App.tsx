import React from 'react';
import logo from './logo.svg';
import { LobbyPage } from './pages/lobby/LobbyExample';
import { Lobby } from './pages/lobby/Lobby';
import { SessionProvider } from './components/sessions/SessionProvider';

const url = ""

function App() {
  return (
    <div className='bg-slate-300 min-h-screen'>
      <SessionProvider url={url}>
        <Lobby/>
      </SessionProvider>
    </div>
  );
}

export default App;
