using PlServer.Domain.Results;
using System.Collections.Immutable;

namespace PlServer.Domain.Nodes.Pipeline;

public class NodeGraphPipeline
{
    private readonly ImmutableArray<INodeGraphPipelineSource> _sources;
    private readonly Dictionary<Type, CommandActivator> _activators;

    public NodeGraphPipeline(IEnumerable<INodeGraphPipelineSource> sources)
    {
        _sources = sources.ToImmutableArray();
        _activators = new Dictionary<Type, CommandActivator>();
    }

    public void Rebuild()
    {
        _activators.Clear();

        var handlers = _sources
            .Select(x => x.GetHandlers())
            .SelectMany(x => x)
            .DistinctBy(x => x.CommandType)
            .ToArray();

        var policies = _sources
            .Select(x => x.GetPolicies())
            .SelectMany(x => x)
            .ToArray();

        var activators = CommandActivatorBuilder.Build(handlers, policies);

        foreach (var activator in activators)
            _activators.Add(activator.Key, activator.Value);
    }

    public UnitResult<NodeErrors> ApplyCommand(NodeGraphContext context, object command)
    {
        var result = _activators.TryGetValue(command.GetType(), out var activator);

        if (result == false)
            return Result.Failure(NodeErrors.UnknownCommand, $"{command} is not supporting");

        return activator!.Invoke(command, context);
    }
}

internal delegate UnitResult<NodeErrors> CommandActivator(object command, NodeGraphContext context);