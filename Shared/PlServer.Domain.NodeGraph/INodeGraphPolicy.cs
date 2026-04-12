using PlServer.Domain.Results;

namespace PlServer.Domain.Nodes;

public interface INodeGraphPolicy
{
    Type CommandType { get; }

    UnitResult<NodeErrors> Validate(NodeGraphContext context, object command);
}

public abstract class NodeGraphPolicy<T> : INodeGraphPolicy where T : class
{
    public Type CommandType { get; } = typeof(T);

    public UnitResult<NodeErrors> Validate(NodeGraphContext context, object command)
    {
        if (command is not T typed)
            return Result.Failure(NodeErrors.Common, "Command is not valid type");

        return Validate(context, typed);
    }

    protected abstract UnitResult<NodeErrors> Validate(NodeGraphContext context, T command);
}