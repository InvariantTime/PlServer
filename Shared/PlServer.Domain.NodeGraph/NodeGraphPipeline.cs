using PlServer.Domain.Results;

namespace PlServer.Domain.Nodes;

public class NodeGraphPipeline
{

    private readonly Dictionary<Type, NodeGraphMiddleware> _polices = new();

    public void Rebuild()
    {
        _polices.Clear();
    }

    public UnitResult<NodeErrors> ApplyCommand<T>(NodeGraphContext context, T command) where T : class
    {
        return Result.Success<NodeErrors>();
    }
}

internal delegate UnitResult<NodeErrors> NodeGraphMiddleware();