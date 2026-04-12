using PlServer.Domain.Results;

namespace PlServer.Domain.Nodes;

public class NodeGraphPipeline
{
    private readonly Dictionary<Type, CommandActivator> _handlers = new();

    public void Rebuild()
    {
        _handlers.Clear();
    }

    public UnitResult<NodeErrors> ApplyCommand<T>(NodeGraphContext context, T command) where T : class
    {
        var result = _handlers.TryGetValue(typeof(T), out var activator);

        if (result == false)
            return Result.Failure(NodeErrors.UnknownCommand, $"{command} is not supporting");

        return activator!.Invoke(command, context);
    }
}

internal delegate UnitResult<NodeErrors> CommandActivator(object command, NodeGraphContext context);