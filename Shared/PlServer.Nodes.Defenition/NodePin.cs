
namespace PlServer.Nodes.Defenition;

public class NodePin
{
    public string Name { get; }

    public PinDirections Direction { get; }

    public VarableTypeDescription Type { get; }

    public NodePin(string name, PinDirections direction, VarableTypeDescription type)
    {
        Name = name;
        Direction = direction;
        Type = type;
    }
}

public enum PinDirections
{
    Input,
    Output
}