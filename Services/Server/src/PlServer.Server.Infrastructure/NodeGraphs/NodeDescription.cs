using PlServer.Domain.Nodes;
using System.Numerics;

namespace PlServer.Server.Infrastructure.NodeGraphs;

public class NodeDescription
{
    public NodeId Id { get; }

    public Point Position { get; }

    public string Name { get; }

    public string DefinitionId { get; }

    public NodeDescription(NodeId id, string name, string definitionId, Vector2 position)
    {
        Id = id;
        Name = name;
        DefinitionId = definitionId;
        Position = new Point(position.X, position.Y);
    }
}

public readonly record struct Point(float X, float Y);