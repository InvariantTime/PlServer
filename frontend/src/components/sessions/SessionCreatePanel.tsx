import { SubmitEvent, useState } from "react";

interface Props {
  closeCallback: (request: object) => void;
}

export const SessionCreatePanel = ({ closeCallback }: Props) => {
  
    const [name, setName] = useState("");

    const onSumbit = (e: SubmitEvent) => {
        e.preventDefault();

        closeCallback({name: name});
    };

  return (
    <div className="flex min-h-full min-w-full items-center justify-center">
      <form
        className="bg-slate-100 border-emerald-900 border-2 rounded-md p-8 w-1/4 relative
                h-1/2 min-h-72 min-w-52 flex flex-col justify-between shadow-xl"
        onSubmit={onSumbit}>

        <div className="flex flex-col p-4">
            <label className="font-bold mb-3 text-xl">name</label>
            <input type="text" 
                value={name} onChange={(e) => setName(e.currentTarget.value)}
                className="min-h-8 bg-slate-100 border-emerald-900 border-2 rounded-md p-2 text-lg mb-5"/>

            <label className="font-bold mb-3 text-xl">plugins</label>
        </div>

        <button
          type="submit"
          className="w-full max-w-md bg-emerald-500 hover:bg-emerald-700 rounded-md p-3">

          <div className="flex items-center justify-center">
            <span className="text-lg text-emerald-100 font-bold">Create</span>
          </div>
        </button>
      </form>
    </div>
  );
};
