using PlServer.Application;
using PlServer.Domain.Nodes;
using PlServer.Server.Domain;
using PlServer.Server.Services.DTOs;
using PlServer.Server.Services.Repositories;

namespace PlServer.Server.Services;

public class NodeGraphService : INodeGraphService
{
    private readonly INodeGraphRepository _repository;
    private readonly IEventDispatcher _dispatcher;
    private readonly INodeGraphPipelineBuilder _builder;

    public NodeGraphService(INodeGraphRepository repository, IEventDispatcher dispatcher, INodeGraphPipelineBuilder builder)
    {
        _repository = repository;
        _dispatcher = dispatcher;
        _builder = builder;
    }

    public Task ApplyCommandAsync(NodeGraphId id, object command)
    {
        var facade = _repository.GetNodeGraphById(id);

        if (facade == null)
            return Task.CompletedTask;

        var result = facade.NodeGraph.ApplyCommand(command);//TODO: handle result

        if (result.IsSuccess == false)
            return Task.CompletedTask;

        return _dispatcher.DispatchEntityEventsAsync(facade.NodeGraph);
    }

    public Task CreateNodeGraphAsync(NodeGraphId id, SessionId sessionId)
    {
        var pipeline = _builder.Build();
        var nodeGraph = NodeGraph.Create(id, pipeline);

        var facade = new NodeGraphFacade(nodeGraph, sessionId, pipeline);
        bool result = _repository.AddNodeGraph(facade);

        if (result == false)
            return Task.CompletedTask;

        return _dispatcher.DispatchEntityEventsAsync(nodeGraph);
    }

    public NodeGraphSummaryDTO? GetNodeGraphDto(NodeGraphId id)
    {
        var graph = _repository.GetNodeGraphById(id);

        if (graph == null)
            return null;

        return NodeGraphSummaryDTO.Create(graph.NodeGraph);
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
