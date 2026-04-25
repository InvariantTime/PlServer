import { useCallback, useEffect, useRef, useState } from "react"
import { Spinner } from "./spinner/Spinner";
import { Navigate, Outlet } from "react-router-dom";
import { Header } from "./header/Header";
import { verify } from "../api/auth/AuthService";
import { Container } from "lucide-react";
import { NotificationProvider } from "./notifying/NotificationProvider";

export const PrivateRoute = () => {

    const [loading, setLoading] = useState(false);
    const [inited, setInited] = useState(false);

    const [notifyContainer, setNotifyContainer] = useState<HTMLElement | null>(null);

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
            <div className="w-full h-[8%]">
                <Header setNotificationContainer={setNotifyContainer}/>
            </div>

            <NotificationProvider container={notifyContainer}>
                <div className="w-full h-[92%]">
                    <Outlet />
                </div>
            </NotificationProvider>
        </div>
    )
}