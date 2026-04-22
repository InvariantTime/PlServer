import { useEffect, useState } from "react"
import { Spinner } from "./spinner/Spinner";
import { Navigate, Outlet } from "react-router-dom";
import { Header } from "./header/Header";
import { verify } from "../api/auth/AuthService";
import { ModalProvider } from "./modals/ModalProvider";
import { SessionProvider } from "./sessions/SessionProvider";

const url = "/ws/lobby";

export const PrivateRoute = () => {

    const [loading, setLoading] = useState(false);
    const [inited, setInited] = useState(false);

    const initUser = async () => {
        const result = await verify();

        setLoading(true);
        setInited(result);
    }

    useEffect(() => {
        initUser();
    }, []);

    if (loading === false) {
        return (
            <div className="w-full h-full flex justify-center items-center">
                <Spinner />
            </div>
        );
    }

    if (inited === false)
        return <Navigate to="/auth" />

    return (
        <div className="w-full h-full">
            <ModalProvider>
                <SessionProvider url={url}>
                    <div className="w-full h-[8%]">
                        <Header />
                    </div>

                    <div className="w-full h-[92%]">
                        <Outlet />
                    </div>
                </SessionProvider>
            </ModalProvider>
        </div>
    )
}