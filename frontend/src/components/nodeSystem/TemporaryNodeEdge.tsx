
const defaultColor = "#FFD700";

interface Props {
    source: {x: number, y: number},
    target: {x: number, y: number}
}

export const TemporaryNodeEdge = ({source, target}: Props) => {

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

    const path = getBezierPath(source.x, source.y, target.x, target.y);

    return (
        <g>
            <path
                d={path}
                className="edge-hover" />

            <path
                d={path}
                stroke={defaultColor}
                className="edge-visible" />
        </g>
    );
}