import { useCallback, useRef, useState } from "react"


export const useNodeSystem = () => {


    const [nodes, setNodes] = useState<string>();
    const [edges, setEdges] = useState<string>();

    const pinRefs = useRef<Record<string, HTMLDivElement | null>>({});

    const addNode = useCallback(() => {

    }, []);

    const removeNode = useCallback(() => {

    }, []);

    const registerPinRef = useCallback(() => {

    }, []);

    const connect = useCallback(() => {

    }, []);


    return {
        nodes,
        addNode,
        removeNode,
        registerPinRef,
        connect
    };
}