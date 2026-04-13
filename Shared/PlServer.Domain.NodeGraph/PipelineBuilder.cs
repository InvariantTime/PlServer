using PlServer.Domain.Results;

namespace PlServer.Domain.Nodes;

internal static class PipelineBuilder //TODO: non static ?
{
    public static CommandActivator Build(IEnumerable<INodeGraphPolicy> policies, Type commandType)
    {
        return (command, context) =>
        {
            return Result.Success<NodeErrors>();
        };
    }
}
