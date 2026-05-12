
using PlServer.Domain.Nodes;
using PlServer.Domain.Nodes.Pipeline;
using PlServer.Domain.Results;

namespace PlServer.Server.Infrastructure.NodeGraphs.Handlers;

public class RemoveNodeHandler : NodeGraphHandler<RemoveNodeCommand>
{
    protected override UnitResult<NodeErrors> Handle(RemoveNodeCommand command, NodeGraphContext context)
    {
        context.RemoveNode(command.NodeId);
        return Result.Success<NodeErrors>();
    }
}
