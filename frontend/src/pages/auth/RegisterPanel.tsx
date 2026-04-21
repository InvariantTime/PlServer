import { SubmitEventHandler, useEffect, useState } from "react"
import "./auth.css"
import { register } from "../../api/auth/AuthService"

interface Props {
    goToLogin: () => void
}

export const RegisterPanel = ({ goToLogin }: Props) => {

    const [name, setName] = useState('');
    const [password, setPassword] = useState('');
    const [cpassword, setCpassword] = useState('');
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        setError(null);
    }, [name, password, cpassword]);

    const onSubmit = async (e: React.SubmitEvent<HTMLFormElement>) => {
        e.preventDefault();

        const result = await register(name, password);

        if (result.type === "error") {
            setError(result.error);
            return;
        }

        //
    };

    return (
        <div className="flex items-center flex-col justify-center my-5">
            <header>
                <h1 className="text-4xl">
                    Register
                </h1>
            </header>

            <form onSubmit={onSubmit} className="w-full max-w-xs">
                <div className="inputBlock">
                    <label htmlFor="name">Name</label>
                    <input id="name" name="name" required
                        value={name} onChange={x => setName(x.currentTarget.value)} />
                </div>

                <div className="inputBlock">
                    <label htmlFor="password">Password</label>
                    <input type="password" id="password" name="password" required
                        value={password} onChange={x => setPassword(x.currentTarget.value)} />
                </div>

                <div className="inputBlock">
                    <label htmlFor="cpassword">Confirm password</label>
                    <input type="password" id="cpassword" name="cpassword" required
                        value={cpassword} onChange={x => setCpassword(x.currentTarget.value)} />
                </div>

                {error !== null && <div className='mt-1 text-center'>
                    <h1 className='text-red-500 font-bold'>{error}</h1>
                </div>}

                <div className='mt-10'>
                    <button type="submit" className="text-xl font-bold rounded-md 
                        text-emerald-100 bg-emerald-500 p-2
                        hover:bg-emerald-700 w-full">
                        submit
                    </button>
                </div>
            </form>

            <span className="font-bold mt-4">
                Already have an account? <a
                    onClick={goToLogin} className="text-blue-500 hover:underline cursor-pointer">login here</a>
            </span>
        </div>
    )
}