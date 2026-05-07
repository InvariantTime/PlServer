
using PlServer.Domain.Nodes;
using PlServer.Domain.Results;

namespace PlServer.Server.Infrastructure.NodeGraphs.Handlers;

public class RemoveNodeHandler : INodeGraphHandler<RemoveNodeCommand>
{
    public UnitResult<NodeErrors> Handle(RemoveNodeCommand command, NodeGraphContext context)
    {
        context.RemoveNode(command.Id);
        return Result.Success<NodeErrors>();
    }
}
