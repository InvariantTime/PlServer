
using PlServer.Domain.Nodes;
using PlServer.Domain.Nodes.Pipeline;
using PlServer.Domain.Results;
using System.Numerics;

INodeGraphPipelineSource[] sources = [new NodeGraphSource()];
var pipeline = new NodeGraphPipeline(sources);


var id = NodeGraphId.New();
NodeGraph graph = new NodeGraph(id, pipeline);
graph.Rebuild();

graph.ApplyCommand(new AddNodeCommand("Node 1", new Vector2(400, 200)));

Console.WriteLine();

class NodeGraphSource : INodeGraphPipelineSource
{
    public IEnumerable<INodeGraphHandler> GetHandlers()
    {
        return [new AddNodeCommandHandler()];
    }

    public IEnumerable<INodeGraphPolicy> GetPolicies()
    {
        return [];
    }
}

record AddNodeCommand(string Name, Vector2 Position);

class AddNodeCommandHandler : NodeGraphHandler<AddNodeCommand>
{
    protected override UnitResult<NodeErrors> Handle(AddNodeCommand command, NodeGraphContext context)
    {
        var id = NodeId.New();

        var node = new Node(command.Name, null!, id);
        node.Move(command.Position);

        bool result = context.AddNode(node);

        if (result == false)
            return Result.Failure(NodeErrors.Common);

        return Result.Success<NodeErrors>();
    }
}