import { User } from "lucide-react";
import "./lobby.css";
import { SessionCreationRequest } from "../../api/sessions/SessionQueries";
import { ChangeEvent, ChangeEventHandler, SubmitEvent, useState } from "react";

interface Props {
  creationCallback: (info: SessionCreationRequest) => void
}

export const SessionCreatePanel = ({ creationCallback }: Props) => {

  const [name, setName] = useState("");
  const [count, setCount] = useState(3);

  const onSumbit = (e: SubmitEvent) => {
    e.preventDefault();

    creationCallback({ name: name, usersCount: count });
  };

  const onChanged = (e: ChangeEvent<HTMLInputElement>) => {
    setCount(parseInt(e.target.value));
  }

  return (
    <form className="bg-slate-100 border-emerald-900 border-2 rounded-md p-8 w-1/4 relative
                h-1/2 min-h-72 min-w-52 flex flex-col justify-between shadow-xl"
      onSubmit={onSumbit}>

      <div className="flex flex-col p-4 gap-8">

        <div>
          <label htmlFor="name">name</label>
          <input type="text" required id="name"
            value={name} onChange={(e) => setName(e.currentTarget.value)}
            className="min-h-8 bg-slate-100 border-emerald-900 border-2 rounded-md p-2 text-lg mb-5" />
        </div>

        <div>
          <label className="count-label" htmlFor="count-container">users count</label>
          <div id="count-container" className="gap-2 flex items-center justify-center mt-2">
            <div className="count-item">
              <input id="1pl" type="radio" name="players" value={1}
                checked={count === 1} onChange={onChanged} />
              <label htmlFor="1pl">1</label>
            </div>

            <div className="count-item">
              <input id="2pl" type="radio" name="players" value={2}
                checked={count === 2} onChange={onChanged} />
              <label htmlFor="2pl">2</label>
            </div>

            <div className="count-item">
              <input id="3pl" type="radio" name="players" value={3} 
                checked={count === 3} onChange={onChanged} />
              <label htmlFor="3pl">3</label>
            </div>

            <div className="count-item">
              <input id="4pl" type="radio" name="players" value={4}
                 checked={count === 4} onChange={onChanged} />
              <label htmlFor="4pl">4</label>
            </div>

            <div className="count-item">
              <input id="5pl" type="radio" name="players" value={5}
                checked= {count === 5} onChange={onChanged} />
              <label htmlFor="5pl">5</label>
            </div>
          </div>
        </div>
      </div>

      <button
        type="submit"
        className="w-full max-w-md bg-emerald-500 hover:bg-emerald-700 rounded-md p-3">

        <div className="flex items-center justify-center">
          <span className="text-lg text-emerald-100 font-bold">Create</span>
        </div>
      </button>
    </form>
  );
};
