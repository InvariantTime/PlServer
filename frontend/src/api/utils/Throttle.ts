import { useCallback, useRef } from "react";

export const useThrottle = <T extends (...args: any[]) => void>(
    callback: T,
    delay: number
): T => {
    const lastCallRef = useRef(0);
    const timeoutRef = useRef<NodeJS.Timeout | null>(null);

    return useCallback((...args: Parameters<T>) => {
        const now = Date.now();
        const remaining = delay - (now - lastCallRef.current);

        if (remaining <= 0) {
            lastCallRef.current = now;
            callback(...args);
        } else {
            if (timeoutRef.current)
                clearTimeout(timeoutRef.current);

            timeoutRef.current = setTimeout(() => {
                lastCallRef.current = Date.now();
                callback(...args);
            }, remaining);
        }
    }, [callback, delay]) as T;
}