import { createContext, useContext } from "react";

export enum NotificationTypes {
    message = 0,
    warning = 1,
    error = 2
}

interface NotificationContextType {
    notify: (message: string, type: NotificationTypes) => void;
}

export const NotificationContext = createContext<NotificationContextType | null>(null);

export const useNotify = () => {
    const ctx = useContext(NotificationContext);

    if (ctx == null)
        throw new Error("notification context is not initialized");

    return ctx?.notify;
}