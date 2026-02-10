import { error } from "console";
import { ComponentType, createContext, ReactNode, useCallback, useContext } from "react";
import { CancellationToken } from "typescript";
import { MessagePanel } from "../../components/modals/MessagePanel";

interface ModalContextType {
    addView: (component: ComponentType<{closeCallback: (value: object) => void}>) => string,
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


export function useDialog<TResult>(panel: ComponentType<{closeCallback: (value: object) => void}>) : () => Promise<TResult> {
    const modals = useModals();

    const callback = useCallback(async () => {
        const id = modals.addView(panel);
        const result = modals.waitForClose(id, null);//TODO: cancellation

        return result as TResult;
    }, [modals]);

    return callback;
}

export function useMessage() : (msg: string, cancellation: CancellationToken | null) => Promise<object> {
    const modals = useModals();

    const callback = useCallback((message: string, cancellation: CancellationToken | null = null) => {
        const id = modals.addView(MessagePanel as ComponentType<{closeCallback: (value: object) => void}>);
        return modals.waitForClose(id, cancellation);
    }, [modals]);

    return callback;
}