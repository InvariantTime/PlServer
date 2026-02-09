import { error } from "console";
import { createContext, ReactNode, useCallback, useContext } from "react";
import { CancellationToken } from "typescript";
import { MessagePanel } from "../../components/modals/MessagePanel";

interface ModalContextType {
    addView: (viewFactory: (closeCallback: (value: object) => void) => ReactNode) => string,
    waitForClose: (id: string, cancellation: CancellationToken | null) => Promise<object>,
    close: (id: string, value: object) => void
}

export const ModalPanelContext = createContext<ModalContextType | null>(null);

export const useModals = (): ModalContextType => {
    const context = useContext(ModalPanelContext);

    if (context == null)
        throw new Error("unable to use modals without context");

    return context;
};


export function useDialog<TResult>(factory: (closeCallback: (result: TResult) => void) => ReactNode) : Promise<TResult> {
    const modals = useModals();

    return null!;//TODO: callback convertion
}

export function useMessage() : (msg: string, cancellation: CancellationToken | null) => Promise<object> {
    const modals = useModals();

    const callback = useCallback((message: string, cancellation: CancellationToken | null = null) => {
        const id = modals.addView((callback) => MessagePanel({closeCallback: callback, message}));
        return modals.waitForClose(id, cancellation);
    }, [modals]);

    return callback;
}