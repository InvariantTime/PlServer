import { Lobby } from "./pages/lobby/Lobby";
import { Session } from "./pages/session/Session";
import { Auth } from "./pages/auth/Auth";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { PrivateRoute } from "./components/PrivateRoute";

function App() {
  return (
    <div className="w-screen h-screen bg-slate-200">
      <BrowserRouter>
        <Routes>
          <Route element={<PrivateRoute />}>
            <Route path="/" element={<Lobby />} />
            <Route path="/session" element={<Session/>}/>
          </Route>

          <Route path="/auth" element={<Auth/>}/>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
