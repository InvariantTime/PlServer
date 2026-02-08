import { ReactNode, useCallback, useMemo, useRef, useState } from "react";
import { ModalPanelContext } from "../../api/modals/ModalPanelApi";
import { CompletionSource } from "../../api/utils/CompletionSource";

interface Props {
  children: ReactNode;
  zStartIndex: number;
}

export const ModalProvider = ({ zStartIndex, children }: Props) => {
  const resolvers = useRef(new Map());
  const [dialogs, setDialogs] = useState<ReactNode[]>([]);

  const addView = useCallback((view: ReactNode, arg: object): string => {
    var id = "";

    do {
      id = Math.random().toString(36).slice(2);
    } while (resolvers.current.has(id) === true);

    resolvers.current.set(id, new CompletionSource());

    return id;
  }, []);

  const waitForClose = useCallback((id: string): Promise<object> => {

    const cs = resolvers.current.get(id);

    if (cs === null) 
        return Promise.reject(`${id} is not opened`);

    return cs.promise;
  }, []);

  const close = useCallback((id: string, value: object) => {

    const cs = resolvers.current.get(id);

    if (cs === null) 
        return Promise.reject(`${id} is not opened`);

    cs.resolve(value);

  }, [])

  const value = useMemo(
    () => ({
      addView: addView,
      waitForClose: waitForClose,
      close: close
    }),
    [],
  );

  return (
    <ModalPanelContext.Provider value={value}>
      {children}
    </ModalPanelContext.Provider>
  );
};
