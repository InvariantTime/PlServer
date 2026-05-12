
using PlServer.Domain.Nodes;
using PlServer.Domain.Nodes.Pipeline;
using PlServer.Domain.Results;
using System.Numerics;

namespace PlServer.Server.Infrastructure.NodeGraphs.Handlers;

public class AddNodeHandler : NodeGraphHandler<AddNodeCommand>
{
    protected override UnitResult<NodeErrors> Handle(AddNodeCommand command, NodeGraphContext context)
    {
        var id = NodeId.New();
        var node = new Node(command.Definition, null!, id);//TODO: get definition
        node.Move(new Vector2(command.Position.X, command.Position.Y));

        var result = context.AddNode(node);

        if (result == true)
            return Result.Success<NodeErrors>();

        return Result.Failure(NodeErrors.Common, "Unable to add node");
    }
}