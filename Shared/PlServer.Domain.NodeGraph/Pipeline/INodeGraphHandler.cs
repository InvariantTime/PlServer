using PlServer.Domain.Results;

namespace PlServer.Domain.Nodes.Pipeline;

public interface INodeGraphHandler
{
    Type CommandType { get; }

    UnitResult<NodeErrors> Handle(object command, NodeGraphContext context);
}

public abstract class NodeGraphHandler<T> : INodeGraphHandler where T : class
{
    public Type CommandType => typeof(T);

    public UnitResult<NodeErrors> Handle(object command, NodeGraphContext context)
    {
        if (command is not T generic)
            return Result.Failure(NodeErrors.UnknownCommand);

        return Handle(generic, context);
    }

    protected abstract UnitResult<NodeErrors> Handle(T command, NodeGraphContext context);
}