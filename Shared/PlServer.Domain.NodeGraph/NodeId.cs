
namespace PlServer.Domain.Nodes;

public readonly record struct NodeId(Guid Id)
{
    public static NodeId New() => new(Guid.NewGuid());
}