
using System.Numerics;

namespace PlServer.Domain.Nodes;

public class Node : Entity<NodeId>
{
    public string DisplayName { get; private set; }

    public Vector2 Position { get; private set; }

    public Node(string name, NodeId id) : base(id)
    {
        DisplayName = name;
    }

    public void Move(Vector2 position)
    {
        Position = position;
    }

    public void SetName(string name)
    {
        DisplayName = name;//TODO: get default name from node definition
    }
}
