using PlServer.Domain.Nodes.Events;
using System.Numerics;

namespace PlServer.Domain.Nodes;

public class NodeGraphContext
{
    private readonly Dictionary<NodeId, Node> _nodes;
    private readonly HashSet<NodeConnection> _connections;
    private readonly List<INodeGraphEvent> _events;

    public ICollection<Node> Nodes => _nodes.Values;

    public NodeGraphId GraphId { get; }

    public IReadOnlyCollection<NodeConnection> Connections => _connections.AsReadOnly();

    public long Version { get; private set; } = 1;

    public NodeGraphContext(NodeGraphId graphId)
    {
        GraphId = graphId;
        _nodes = new Dictionary<NodeId, Node>();
        _connections = new HashSet<NodeConnection>();
        _events = new List<INodeGraphEvent>();
    }

    public bool AddNode(Node node)
    {
        bool result = _nodes.TryAdd(node.Key, node);

        if (result == true)
        {
            Version++;
            _events.Add(new NodeAddedEvent(GraphId, Version, DateTime.UtcNow, node.Key));
        }

        return result;
    }

    public void AddConnection(NodeConnection connection)
    {
        var result = _connections.Add(connection);

        if (result == true)
        {
            Version++;
            _events.Add(new ConnectionAddedEvent(GraphId, Version, DateTime.UtcNow, connection));
        }
    }

    public void RemoveNode(NodeId node)
    {
        var result = _nodes.Remove(node);

        if (result == true)
        {
            Version++;
            _events.Add(new NodeRemovedEvent(GraphId, Version, DateTime.UtcNow, node));
        }
    }

    public void RemoveConnection(NodeConnectionPart target)
    {
        var result = _connections.FirstOrDefault(x => x.Target == target);

        if (result == null)
            return;

        _connections.Remove(result);

        Version++;
        _events.Add(new ConnectionRemovedEvent(GraphId, Version, DateTime.UtcNow, target));
    }

    public void MoveNode(NodeId nodeId, Vector2 position)
    {
        _nodes.TryGetValue(nodeId, out var node);

        if (node == null)
            return;

        node.Move(position);

        Version++;
        _events.Add(new NodeMovedEvent(GraphId, Version, DateTime.UtcNow, nodeId, position));
    }

    public void ChangeParameter()//TODO
    {

    }
}
