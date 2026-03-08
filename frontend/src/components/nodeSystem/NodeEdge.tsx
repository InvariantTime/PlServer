import { NodeConnection } from "../../api/nodes/NodeConnection";
import "./Node.css"


interface Props {
    connection: NodeConnection,
    getPinPosition: (nodeId: string, pinId: string) => ({x: number, y: number} | null)
}

const defaultColor = "#3b82f6";

export const NodeEdge = ({ connection, getPinPosition }: Props) => {

    const getBezierPath = (
        sourceX: number,
        sourceY: number,
        targetX: number,
        targetY: number
    ): string => {
        const deltaX = Math.abs(targetX - sourceX);
        const deltaY = Math.abs(targetY - sourceY);

        const offset = Math.min(
            Math.max(deltaX * 0.5, deltaY * 0.5),
            150
        );

        const cp1x = sourceX;
        const cp1y = sourceY + offset;

        const cp2x = targetX;
        const cp2y = targetY - offset;

        return `M ${sourceX} ${sourceY} C ${cp1x} ${cp1y}, ${cp2x} ${cp2y}, ${targetX} ${targetY}`;
    };

    const source = getPinPosition(connection.source.nodeId, connection.source.pinId) ?? {x: 0, y: 0};
    const target = getPinPosition(connection.target.nodeId, connection.target.pinId) ?? {x: 0, y: 0};

    const path = getBezierPath(source.x, source.y, target.x, target.y);

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
                d={path}
                className="edge-hover" />

            <path
                d={path}
                stroke={startColor != null && endColor != null ? "url(#curveGradient)" : defaultColor}
                className="edge-visible" />
        </g>
    )
}