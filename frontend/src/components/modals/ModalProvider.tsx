import { ComponentType, MouseEvent, ReactNode, useCallback, useMemo, useRef, useState } from "react";
import { ModalPanelContext } from "../../api/modals/ModalPanelApi";
import { CompletionSource } from "../../api/utils/CompletionSource";
import { createPortal } from "react-dom";
import { CancellationToken } from "typescript";
import "./ModalPanel.css";

interface Props {
  children: ReactNode;
}

type PanelInfo = {
  component: ComponentType<{closeCallback: (value: object) => void}>,
  id: string
}

export const ModalProvider = ({ children }: Props) => {

  const [panels, setPanels] = useState<PanelInfo[]>([]);
  const sources = useRef<Map<string, CompletionSource<object>>>(new Map());
  
  const addView = useCallback((component: ComponentType<{closeCallback: (value: object) => void}>): string => {
    var id = "";

    do {
      id = Math.random().toString(36).slice(2);
    } while (sources.current.has(id) === true);

    const cs = new CompletionSource<object>();

    sources.current.set(id, cs);

    setPanels(prev => [...prev, {id, component}]);

    (document.activeElement as HTMLElement)?.blur();

    cs.promise.finally(() => {
      sources.current.delete(id);
      setPanels(prev => prev.filter(x => x.id != id));
    });

    return id;
  }, []);
  
  const waitForClose = useCallback((id: string, cancellation: CancellationToken | null): Promise<object> => {
    const cs = sources.current.get(id);

    if (cs === null) 
      return Promise.reject(`${id} is not opened`);

    return cs!.promise;
  }, []);

  const close = useCallback((id: string, value: object) => {
    const cs = sources.current.get(id);

    if (cs === null) 
      return Promise.reject(`${id} is not opened`);

    cs!.resolve(value);

  }, []);

  const value = useMemo(
    () => ({
      addView: addView,
      waitForClose: waitForClose,
      close: close,
    }), []);
  
    const panelView = (info: PanelInfo) => {

      const onOverlayClick = (event: MouseEvent) => {
        if (event.target === event.currentTarget)
          close(info.id, {});
      }

    return createPortal((
      <div className="overlay" onClick={onOverlayClick}>
        <info.component closeCallback={(value) => close(info.id, value)}/>
      </div>
    ), document.body)
  }

  return (
    <ModalPanelContext.Provider value={value}>
      {children}

      {panels.map(panelView)}
    </ModalPanelContext.Provider>
  );
};
