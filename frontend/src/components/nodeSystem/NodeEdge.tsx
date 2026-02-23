import "./Node.css"


interface Props {
    startX: number,
    startY: number,
    endX: number,
    endY: number,
    startColor?: string,
    endColor?: string
}

const defaultColor = "#3b82f6";

export const NodeEdge = ({ startX, startY, endX, endY, startColor, endColor }: Props) => {

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

    const path = getBezierPath(startX, startY, endX, endY)

    return (

        <g>
            {(startColor != null && endColor != null) &&
            <defs>
                <linearGradient
                    id="curveGradient"
                    x1={startX}
                    y1={startY}
                    x2={endX}
                    y2={endY}
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