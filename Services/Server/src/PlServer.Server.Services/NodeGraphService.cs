using PlServer.Application;
using PlServer.Domain.Nodes;
using PlServer.Server.Domain;
using PlServer.Server.Services.Repositories;

namespace PlServer.Server.Services;

public class NodeGraphService : INodeGraphService
{
    private readonly INodeGraphRepository _repository;
    private readonly IEventDispatcher _dispatcher;

    public NodeGraphService(INodeGraphRepository repository, IEventDispatcher dispatcher)
    {
        _repository = repository;
        _dispatcher = dispatcher;
    }

    public Task ApplyCommandAsync(NodeGraphId id, object command)
    {
        return Task.CompletedTask;
    }

    public Task CreateNodeGraphAsync(NodeGraphId id, SessionId sessionId)
    {
        var pipeline = new NodeGraphPipeline();
        var nodeGraph = new NodeGraph(id, pipeline);

        var facade = new NodeGraphFacade(nodeGraph, sessionId, pipeline);
        bool result = _repository.AddNodeGraph(facade);

        if (result == false)
            return Task.CompletedTask;

        return _dispatcher.DispatchEntityEventsAsync(nodeGraph);
    }

    public Task RemoveNodeGraphAsync(NodeGraphId id)
    {
        bool result = _repository.RemoveNodeGraph(id);

        if (result == false)
            return Task.CompletedTask;

        //TODO: dispatch event

        return Task.CompletedTask;
    }
}
