interface Props {
  message: string;
  closeCallback: (value: object) => void
}

export const MessagePanel = ({ closeCallback, message }: Props) => {
  return (
    <div className="flex justify-center items-center min-h-full min-w-full">
      <div className="bg-slate-100 border-emerald-900 border-2 rounded-md p-8 w-1/4  
        h-1/3 min-h-60 min-w-52 flex flex-col justify-between shadow-xl">
        <div className="font-bold text-xl text-center pt-8">
          <p>{message}</p>
        </div>

        <button
          className="w-full max-w-md bg-emerald-500 hover:bg-emerald-700 rounded-md p-3"
          onClick={closeCallback}>
          <div className="flex items-center justify-center">
            <span className="text-lg text-emerald-100 font-bold">
              Ok
            </span>
          </div>
        </button>
      </div>
    </div>
  );
};
