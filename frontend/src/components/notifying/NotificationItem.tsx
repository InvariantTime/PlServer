import { BadgeAlert, BadgeInfo, BadgeX } from "lucide-react"
import { NotificationTypes } from "../../api/notifying/Notification"
import "./notification.css"
import { AnimationEvent, useState } from "react"

const progressAnimationName = 'timeout';
const stopAnimationName = 'slideOutLeft';

const styles = {
    [NotificationTypes.error]: { body: "bg-red-500" },
    [NotificationTypes.message]: { body: "bg-blue-500" },
    [NotificationTypes.warning]: { body: "bg-yellow-500" }
}

const icons = {
    [NotificationTypes.error]: (<BadgeX strokeWidth={2} color="white" />),
    [NotificationTypes.message]: (<BadgeAlert strokeWidth={2} color="white" />),
    [NotificationTypes.warning]: (<BadgeInfo strokeWidth={2} color="white" />)
}

interface Props {
    message: string,
    type: NotificationTypes,
    remove: () => void
}

export const NotificationItem = ({ message, type, remove }: Props) => {

    const [startStopAnimation, setStartStopAnimation] = useState(false);
    const [progressPaused, setProgressPaused] = useState(false);

    const style = styles[type];
    const icon = icons[type];

    const onAnimationEnd = (e: AnimationEvent<HTMLElement>) => {
        if (e.animationName === progressAnimationName) {
            setStartStopAnimation(true);
        }
        else if (e.animationName === stopAnimationName) {
            remove();
        }
    }

    const onMouseEnter = () => {
        setProgressPaused(true);
    }

    const onMouseLeave = () => {
        setProgressPaused(false);
    }

    return (
        <div
            onMouseEnter={onMouseEnter}
            onMouseLeave={onMouseLeave}
            className={`overflow-hidden shadow-xl
            border-slate-100  rounded-lg border-2 px-2
            min-w-52 max-w-64 ${style.body}
            ${startStopAnimation ? 'animate-slideOutLeft' : 'animate-slideInLeft'}`}
            onAnimationEnd={onAnimationEnd}>
                
            <div className={`text-slate-100 gap-4 p-1 pt-2 flex
                font-medium text-center`}>
                <span>{icon}</span>
                <span>{message}</span>
            </div>

            <div className="rounded-br-lg rounded-bl-lg  w-full p-1">
                <div className="w-full h-1 bg-slate-200 animate-progress"
                    style={{ animationPlayState: progressPaused ? 'paused' : 'running' }}
                    onAnimationEnd={onAnimationEnd} />
            </div>
        </div>
    )
}