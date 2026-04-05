namespace PlServer.Domain.Nodes;

public interface INodeDefinition
{
    public string Name { get; }

    public IReadOnlyCollection<NodePin> Inputs { get; }

    public IReadOnlyCollection<NodePin> Outputs { get; }
}
