using PlServer.Domain.Nodes;
using PlServer.Domain.Nodes.Pipeline;
using PlServer.Server.Domain;

namespace PlServer.Server.Services;

public class NodeGraphFacade
{
    public NodeGraph NodeGraph { get; }

    public NodeGraphPipeline Pipeline { get; }

    public NodeGraphId Id => NodeGraph.Key;

    public SessionId SessionId { get; }

    public NodeGraphFacade(NodeGraph nodeGraph, SessionId sessionId, NodeGraphPipeline pipeline)
    {
        NodeGraph = nodeGraph;
        Pipeline = pipeline;
        SessionId = sessionId;
    }
}
