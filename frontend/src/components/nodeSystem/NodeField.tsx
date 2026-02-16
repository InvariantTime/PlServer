import { MouseEvent, useState, WheelEvent } from "react"

export const NodeField = () => {

    const [viewport, setViewport] = useState({ x: 0, y: 0, zoom: 1 });
    const [isPanning, setIsPanning] = useState(false);

    const onMouseMove = (e: MouseEvent<HTMLDivElement>) => {

        if (isPanning === true) {
            const x = viewport.x + e.movementX;
            const y = viewport.y + e.movementY;
            const zoom = viewport.zoom;

            setViewport({ x: x, y: y, zoom: zoom });
        }
    }

    const onMouseWheel = (e: WheelEvent<HTMLDivElement>) => {
        const zoomSpeed = 0.1;
        const newZoom = e.deltaY > 0 
            ? viewport.zoom * (1 - zoomSpeed)
            : viewport.zoom * (1 + zoomSpeed);

        const mouseX = e.clientX;
        const mouseY = e.clientY;
        const newX = mouseX - (mouseX - viewport.x) * (newZoom / viewport.zoom);
        const newY = mouseY - (mouseY - viewport.y) * (newZoom / viewport.zoom);
    
        setViewport({ x: newX, y: newY, zoom: newZoom });
    }

    const onMouseDown = (e: MouseEvent<HTMLDivElement>) => {

        if (e.button === 0) {
            setIsPanning(true);
        }
    }

    const onUnfocus = () => {
        setIsPanning(false);
    }

    return (
        <div className="bg-slate-400 overflow-hidden relative touch-none w-full h-full"
            onMouseMove={onMouseMove}
            onMouseDown={onMouseDown}
            onWheel={onMouseWheel}
            onMouseUp={onUnfocus}
            onMouseLeave={onUnfocus}>
            <div className=""
                style={{ transform: `translate(${viewport.x}px, ${viewport.y}px) scale(${viewport.zoom})` }}>

                <div className="w-16 h-16 bg-red-500">
                </div>
            </div>
        </div>
    )
}