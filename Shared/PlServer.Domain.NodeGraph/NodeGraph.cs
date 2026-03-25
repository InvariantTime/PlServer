
using PlServer.Domain.Results;

namespace PlServer.Domain.Nodes;

public class NodeGraph
{
    private readonly Dictionary<NodeId, Node> _nodes;

    public NodeGraph()
    {
        _nodes = new Dictionary<NodeId, Node>();
    }

    public UnitResult<NodeErrors> AddNode(Node node)
    {
        bool result = _nodes.TryAdd(node.Key, node);

        if (result == false)
            return Result.Failure(NodeErrors.Common, "Unable to add node");

        return Result.Success<NodeErrors>();
    }

    public UnitResult<NodeErrors> RemoveNode(NodeId id)
    {
        if (_nodes.ContainsKey(id) == false)
            return Result.Failure(NodeErrors.NoExists, $"There is no node with id: {id}");

        bool result = _nodes.Remove(id);

        return Result.Check(result, NodeErrors.Common, $"Unable to remove node {id}");
    }
}
