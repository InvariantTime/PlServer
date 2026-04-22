import { useEffect, useState } from 'react';
import './auth.css'
import { login } from '../../api/auth/AuthService';
import { useNavigate } from 'react-router-dom';


interface Props {
    goToRegister: () => void
}

export const LoginPanel = ({ goToRegister }: Props) => {

    const [name, setName] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState<string | null>(null);
    const navigation = useNavigate();

    useEffect(() => {
        setError(null);
    }, [name, password]);

    const onSubmit = async (e: React.SubmitEvent<HTMLFormElement>) => {
        e.preventDefault();

        const result = await login(name, password);

        if (result.type === "error") {
            setError(result.error);
            return;
        }

        navigation("/");
    }

    return (
        <div className="flex items-center flex-col justify-center my-5">
            <header>
                <h1 className="text-4xl">
                    Login
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
                Don't have an account? <a
                    onClick={goToRegister} className="text-blue-500 hover:underline cursor-pointer">register here</a>
            </span>
        </div>
    )
}