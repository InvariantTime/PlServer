
using PlServer.Nodes.Defenition;
using System.Numerics;

namespace PlServer.Domain.Nodes;

public class Node : Entity<NodeId>
{
    public string DisplayName { get; private set; }

    public Vector2 Position { get; private set; }

    public NodeDefinitionDescription Definition { get; }

    public Dictionary<string, object> Values { get; }

    public Node(string name, NodeDefinitionDescription definition, NodeId id) : base(id)
    {
        DisplayName = name;
        Definition = definition;
        Values = new();
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