using System.Collections.Immutable;

namespace PlServer.Nodes.Defenition;

public class NodeDefinitionDescription
{
    public string Name { get; }

    public NodeDefinitionId Id { get; }

    public ImmutableArray<NodePin> Inputs { get; }

    public ImmutableArray<NodePin> Outputs { get; }

    public NodeDefinitionDescription(
        string name, NodeDefinitionId id, IEnumerable<NodePin> pins)
    {
        Name = name;
        Id = id;
        Inputs = pins
            .Where(x => x.Direction == PinDirections.Input)
            .ToImmutableArray();

        Outputs = pins
            .Where(x => x.Direction == PinDirections.Output)
            .ToImmutableArray();
    }
}
