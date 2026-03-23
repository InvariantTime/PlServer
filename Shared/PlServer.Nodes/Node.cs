
using PlServer.Domain;
using System.Numerics;

namespace PlServer.Nodes;

public class Node : Entity<Guid>
{
    public string DisplayName { get; private set; }

    public Vector2 Position { get; private set; }

    public Node(string name, Guid id) : base(id)
    {
        DisplayName = name;
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }

    public void SetName(string name)
    {
        DisplayName = name;
    }
}
