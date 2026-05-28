using System.Collections.Immutable;

namespace PlServer.Nodes.Defenition;

public class NodeDefinitionDescription
{
    public string Name { get; }

    public string Id { get; }

    public ImmutableArray<NodePin> Inputs { get; }

    public ImmutableArray<NodePin> Outputs { get; }

    public ImmutableDictionary<string, NodeValueType> Values { get; }

    public VisualGraph Graph { get; }

    public NodeDefinitionDescription(
        string name, string id, 
        IEnumerable<NodePin> pins, 
        IDictionary<string, NodeValueType> values)
    {
        Name = name;
        Id = id;
        Inputs = pins
            .Where(x => x.Direction == PinDirections.Input)
            .ToImmutableArray();

        Outputs = pins
            .Where(x => x.Direction == PinDirections.Output)
            .ToImmutableArray();

        Values = values.ToImmutableDictionary();
    }
}

public record struct NodeValueType();

public record struct VisualGraph();