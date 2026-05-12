
using PlServer.Domain.Nodes;
using PlServer.Domain.Nodes.Pipeline;
using PlServer.Domain.Results;
using System.Numerics;

namespace PlServer.Server.Infrastructure.NodeGraphs.Handlers;

public class MoveNodeHandler : NodeGraphHandler<MoveNodeCommand>
{
    protected override UnitResult<NodeErrors> Handle(MoveNodeCommand command, NodeGraphContext context)
    {
        context.MoveNode(command.NodeId, new Vector2(command.NewPosition.X, command.NewPosition.Y));
        return Result.Success<NodeErrors>();
    }
}
