namespace PlServer.Domain.Nodes;

public class NodeGraphContext //TODO: events
{
    private readonly Dictionary<NodeId, Node> _nodes;
    private readonly HashSet<NodeConnection> _connections;

    public ICollection<Node> Nodes => _nodes.Values;

    public IReadOnlyCollection<NodeConnection> Connections => _connections.AsReadOnly();

    public NodeGraphContext()
    {
        _nodes = new Dictionary<NodeId, Node>();
        _connections = new HashSet<NodeConnection>();
    }

    public bool AddNode(Node node)
    {
        return _nodes.TryAdd(node.Key, node);
    }

    public void AddConnection(NodeConnection connection)
    {
        _connections.Add(connection);
    }

    public void RemoveNode(NodeId node)
    {
        _nodes.Remove(node);
    }

    public void RemoveConnection(NodeConnectionPart target)
    {
        var result = _connections.FirstOrDefault(x => x.Target == target);

        if (result == null)
            return;

        _connections.Remove(result);
    }
}
