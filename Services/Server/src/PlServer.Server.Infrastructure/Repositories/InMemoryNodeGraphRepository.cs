
using PlServer.Domain.Nodes;
using PlServer.Server.Services.Repositories;
using System.Collections.Concurrent;

namespace PlServer.Server.Infrastructure.Repositories;

public class InMemoryNodeGraphRepository : INodeGraphRepository
{
    private readonly ConcurrentDictionary<NodeGraphId, NodeGraph> _graphs = new();

    public bool AddNodeGraph(NodeGraph graph)
    {
        return _graphs.TryAdd(graph.Key, graph);
    }

    public ICollection<NodeGraph> GetAll()
    {
        return _graphs.Values;
    }

    public NodeGraph? GetNodeGraphById(NodeGraphId id)
    {
        _graphs.TryGetValue(id, out var result);
        return result;
    }

    public bool RemoveNodeGraph(NodeGraphId id)
    {
        return _graphs.TryRemove(id, out _);
    }
}
