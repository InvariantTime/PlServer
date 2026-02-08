import { error } from "console";
import { createContext, useContext } from "react";

interface ModalContextType {
    addView: (view: React.ReactNode, arg: object) => string,
    waitForClose: (id: string) => Promise<object>,
    close: (id: string, value: object) => void
}

export interface ModalPanel {

}

export const ModalPanelContext = createContext<ModalContextType | null>(null);

export const useModals = (): ModalContextType => {
    const context = useContext(ModalPanelContext);

    if (context == null)
        throw new Error("unable to use modals without context");

    return context;
};


export function useDialog<TResult, TValue>(value?: TValue) : Promise<TResult> {
    return null!;
}

export async function useMessage<TValue extends ModalPanel>() : Promise<void> {
    const modals = useModals();

    
}