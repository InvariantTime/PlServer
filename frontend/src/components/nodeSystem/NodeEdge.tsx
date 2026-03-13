import { NodeConnection } from "../../api/nodes/NodeConnection";
import "./Node.css"


interface Props {
    connection: NodeConnection,
    getPinPosition: (nodeId: string, pinId: string) => ({x: number, y: number} | null),
    onEdgeClick: (e: React.MouseEvent, id: string) => void
}

const defaultColor = "#3b82f6";

export const NodeEdge = ({ connection, getPinPosition, onEdgeClick }: Props) => {

    const source = getPinPosition(connection.target.nodeId, connection.target.pinId);
    const target = getPinPosition(connection.source.nodeId, connection.source.pinId);

    if (source === null || target === null)
        return null;

    const dx = target.x - source.x;
        const controlOffset = Math.abs(dx) * 0.5;
  
        const cp1x = source.x + controlOffset;
        const cp1y = source.y;
        const cp2x = target.x - controlOffset;
        const cp2y = target.y;

        const pathData = `M ${source.x} ${source.y} C ${cp1x} ${cp1y}, ${cp2x} ${cp2y}, ${target.x} ${target.y}`;

    const startColor = null;
    const endColor = null;//TODO: colors

    return (
        <g>
            {(startColor != null && endColor != null) &&
            <defs>
                <linearGradient
                    id="curveGradient"
                    x1={source.x}
                    y1={source.y}
                    x2={target.x}
                    y2={target.y}
                    gradientUnits="userSpaceOnUse">

                    <stop offset="0%" stopColor={startColor} />
                    <stop offset="100%" stopColor={endColor} />
                </linearGradient>
            </defs>}

            <path
                d={pathData}
                className="edge-hover cursor-pointer"
                onClick={(e) => onEdgeClick(e, connection.id)} />

            <path
                d={pathData}
                stroke={startColor != null && endColor != null ? "url(#curveGradient)" : defaultColor}
                className="edge-visible" />
        </g>
    )
}