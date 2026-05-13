using PlServer.Domain.Nodes;
using PlServer.Domain.Nodes.Pipeline;
using PlServer.Domain.Results;

namespace PlServer.Server.Infrastructure.NodeGraphs.Handlers;

public class AddConnectionHandler : NodeGraphHandler<AddConnectionCommand>
{
    protected override UnitResult<NodeErrors> Handle(AddConnectionCommand command, NodeGraphContext context)
    {
        context.AddConnection(command.Connection);
        return Result.Success<NodeErrors>();
    }
}
