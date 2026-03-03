import { NodeViewport } from "./NodeViewport"

interface Props {
    viewport: NodeViewport
}

export const NodeFieldBackground = ({viewport}: Props) => {

    return (
        <div className="node-field-background"
                style={{
                    '--vx': viewport.x,
                    '--vy': viewport.y,
                    backgroundSize: `${Math.max(4, Math.min(35, 20 * viewport.zoom))}px ${Math.max(4, Math.min(35, 20 * viewport.zoom))}px`,
                    opacity: viewport.zoom < 0.3 ? 0.3 : 1
                } as React.CSSProperties} />
    )
}