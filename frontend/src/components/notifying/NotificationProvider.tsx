import { ReactElement, useCallback, useEffect, useState } from "react";
import { NotificationContext, NotificationTypes } from "../../api/notifying/Notification";
import { createPortal } from "react-dom";
import { NotificationItem } from "./NotificationItem";

const maxNotifications = 3;

interface NotificationEntry {
    id: string,
    message: string,
    type: NotificationTypes,
    remove: () => void
}

interface Props {
    children: ReactElement,
    container: HTMLElement | null
}

export const NotificationProvider = ({children, container}: Props) => {

    const [notifications, setNotifications] = useState<NotificationEntry[]>([]);

    const removeById = useCallback((id: string) => {
        setNotifications(prev => prev.filter(x => x.id !== id));
    }, [notifications]);
    
    const notify = useCallback((message: string, type: NotificationTypes) => {
        const id = `${Date.now()}-${Math.random().toString(36).slice(2)}`;
        
        const remove = () => {
            removeById(id);
        };

        setNotifications(prev => [...prev, {id: id, message: message, type: type, remove: remove}]);

    }, [notifications, removeById]);

    return (
        <NotificationContext.Provider value={{notify}}>
            {children}

            {container && createPortal(
                (<div className="flex flex-col items-center gap-2">
                    {notifications.map((n) => {
                        return (
                            <NotificationItem 
                                message={n.message} 
                                type={n.type}
                                remove={n.remove}
                                key={n.id}/>
                        )
                    })}
                </div>),
                container
            )}
        </NotificationContext.Provider>
    );
}