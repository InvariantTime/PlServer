namespace PlServer.Domain.Nodes;

public class NodeGraphContext //TODO: events
{
    private readonly Dictionary<NodeId, Node> _nodes;
    private readonly HashSet<NodeConnection> _connections;

    public ICollection<Node> Nodes => _nodes.Values;

    public IReadOnlyCollection<NodeConnection> Connections => _connections.AsReadOnly();

    public long Version { get; private set; } = 1;

    public NodeGraphContext()
    {
        _nodes = new Dictionary<NodeId, Node>();
        _connections = new HashSet<NodeConnection>();
    }

    public bool AddNode(Node node)
    {
        bool result = _nodes.TryAdd(node.Key, node);

        if (result == true)
            Version++;

        return result;
    }

    public void AddConnection(NodeConnection connection)
    {
        var result = _connections.Add(connection);

        if (result == true)
            Version++;
    }

    public void RemoveNode(NodeId node)
    {
        var result = _nodes.Remove(node);

        if (result == true)
            Version++;
    }

    public void RemoveConnection(NodeConnectionPart target)
    {
        var result = _connections.FirstOrDefault(x => x.Target == target);

        if (result == null)
            return;

        _connections.Remove(result);
        Version++;
    }
}
