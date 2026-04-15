namespace PlServer.Domain.Nodes;

public readonly record struct NodeGraphId(Guid Id)
{
    public static NodeGraphId New() => new(Guid.NewGuid());
}
