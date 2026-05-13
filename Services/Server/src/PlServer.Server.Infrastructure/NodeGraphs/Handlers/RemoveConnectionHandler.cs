
using PlServer.Domain.Nodes;
using PlServer.Domain.Nodes.Pipeline;
using PlServer.Domain.Results;

namespace PlServer.Server.Infrastructure.NodeGraphs.Handlers;

public class RemoveConnectionHandler : NodeGraphHandler<RemoveConnectionCommand>
{
    protected override UnitResult<NodeErrors> Handle(RemoveConnectionCommand command, NodeGraphContext context)
    {
        context.RemoveConnection(command.Target);
        return Result.Success<NodeErrors>();
    }
}
