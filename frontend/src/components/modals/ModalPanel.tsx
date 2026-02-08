import React from "react"
import "./ModalPanel.css"

interface Props {
    children: React.ReactNode,
    isOpen: boolean,
}

export const ModalPanel = ({children, isOpen}: Props) => {

    return ( 
        <div className="overlay">
            <div>
                
            </div>
        </div>
    );
}