
using PlServer.Domain.Nodes;
using PlServer.Domain.Results;

namespace PlServer.Server.Infrastructure.NodeGraphs.Handlers;

public class AddNodeHandler : INodeGraphHandler<AddNodeCommand>
{
    public UnitResult<NodeErrors> Handle(AddNodeCommand command, NodeGraphContext context)
    {
        var id = NodeId.New();
        var node = new Node(command.Definition, null!, id);//TODO: get definition

        var result = context.AddNode(node);

        if (result == true)
            return Result.Success<NodeErrors>();

        return Result.Failure(NodeErrors.Common, "Unable to add node");
    }
}