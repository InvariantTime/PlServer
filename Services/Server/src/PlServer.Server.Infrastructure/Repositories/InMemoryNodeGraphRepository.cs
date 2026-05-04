using PlServer.Domain.Nodes;
using PlServer.Server.Domain;
using PlServer.Server.Services;
using PlServer.Server.Services.Repositories;
using System.Collections.Concurrent;

namespace PlServer.Server.Infrastructure.Repositories;

public class InMemoryNodeGraphRepository : INodeGraphRepository
{
    private readonly ConcurrentDictionary<NodeGraphId, NodeGraphFacade> _graphs = new();
    private readonly ConcurrentDictionary<SessionId, NodeGraphFacade> _sessionIndex = new();

    public bool AddNodeGraph(NodeGraphFacade graph)
    {
        return _graphs.TryAdd(graph.Id, graph);
    }

    public ICollection<NodeGraphFacade> GetAll()
    {
        return _graphs.Values;
    }

    public NodeGraphFacade? GetBySessionId(SessionId session)
    {
        _sessionIndex.TryGetValue(session, out var facade);
        return facade;
    }

    public NodeGraphFacade? GetNodeGraphById(NodeGraphId id)
    {
        _graphs.TryGetValue(id, out var result);
        return result;
    }

    public bool RemoveNodeGraph(NodeGraphId id)
    {
        return _graphs.TryRemove(id, out _);
    }
}
