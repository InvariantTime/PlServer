using PlServer.Domain.Nodes.Pipeline;
using PlServer.Server.Services;

namespace PlServer.Server.Infrastructure.NodeGraphs;

public class NodeGraphPipelineBuilder : INodeGraphPipelineBuilder
{
    public NodeGraphPipeline Build()
    {
        return new NodeGraphPipeline();
    }
}
