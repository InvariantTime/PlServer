
using System.Numerics;

namespace PlServer.Domain.Nodes;

public class Node : Entity<NodeId>
{
    public string DisplayName { get; private set; }

    public Vector2 Position { get; private set; }

    public INodeDefinition Definition { get; }

    public Node(string name, INodeDefinition definition, NodeId id) : base(id)
    {
        DisplayName = name;
        Definition = definition;
    }

    public void Move(Vector2 position)
    {
        Position = position;
    }

    public void SetName(string? name)
    {
        DisplayName = name ?? Definition.Name;
    }
}