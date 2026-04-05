namespace PlServer.Domain.Nodes;

public readonly record struct NodePinId(Guid Id)
{
    public static NodePinId New() => new(Guid.NewGuid());
}