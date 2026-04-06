
using PlServer.Domain.Results;

namespace PlServer.Domain.Nodes;

public class NodeGraph
{
    private readonly Dictionary<NodeId, Node> _nodes;
    private readonly HashSet<NodeConnection> _connections;

    public NodeGraph()
    {
        _nodes = new Dictionary<NodeId, Node>();
        _connections = new HashSet<NodeConnection>();
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

    public UnitResult<NodeErrors> AddConnection(NodeConnection connection)
    {
        if (_connections.Contains(connection) == true)
            return Result.Failure(NodeErrors.AlreadyExists, "There is already such connection");

        //TODO: validation types and e.t.c

        return Result.Success<NodeErrors>();
    }

    public UnitResult<NodeErrors> RemoveConnection(NodeConnectionPart target)
    {
        var other = _connections.FirstOrDefault(x => x.Target == target);

        if (other == null)
            return Result.Failure(NodeErrors.NoExists);

        _connections.Remove(other);

        return Result.Success<NodeErrors>();
    }
}
