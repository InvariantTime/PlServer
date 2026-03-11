
const defaultColor = "#FFD700";
const circleColor = "#f0cb05";

interface Props {
    sourceX: number,
    sourceY: number,
    targetX: number,
    targetY: number
}

export const TemporaryNodeEdge = ({sourceX, sourceY, targetX, targetY}: Props) => {

        const dx = targetX - sourceX;
        const controlOffset = Math.abs(dx) * 0.5;
  
        const cp1x = sourceX + controlOffset;
        const cp1y = sourceY;
        const cp2x = targetX - controlOffset;
        const cp2y = targetY;

        const pathData = `M ${sourceX} ${sourceY} C ${cp1x} ${cp1y}, ${cp2x} ${cp2y}, ${targetX} ${targetY}`;

    return (
        <g>
            <path
                d={pathData}
                stroke={defaultColor}
                className="edge-visible" />

            <circle
                className="pointer-events-none"
                cx={targetX}
                cy={targetY}
                r={5}
                fill={circleColor}/>
        </g>
    );
}