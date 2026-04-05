
namespace PlServer.Domain.Nodes;

public class NodePin : Entity<NodePinId>
{
    public string Name { get; }

    public PinTypes Types { get; }

    public PinDirections Direction { get; }

    public NodePin(NodePinId key, string name, PinTypes types, PinDirections direction) : base(key)
    {
        Name = name;
        Types = types;
        Direction = direction;
    }
}

public enum PinDirections
{
    Input = 0,
    Output = 1
}

public enum PinTypes
{
    Int = 0,
    Float = 1,
    String = 2
}