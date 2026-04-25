import { ReactElement } from "react";
import "./layout.css";

interface Props {
    children?: ReactElement,
    onClick?: () => void
}

export const Layout = ({ children, onClick }: Props) => {
    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 modal">
            <div onClick={onClick} className="w-full h-full flex items-center justify-center layout-animated">
                <div className="contents panel-animated"
                    onClick={e => e.stopPropagation()}>
                    {children}
                </div>
            </div>
        </div>
    );
}