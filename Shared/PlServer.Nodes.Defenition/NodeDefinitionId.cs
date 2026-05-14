
namespace PlServer.Nodes.Defenition;

public readonly record struct NodeDefinitionId(Guid Id)
{
    public static NodeDefinitionId New() => new(Guid.NewGuid());
}
