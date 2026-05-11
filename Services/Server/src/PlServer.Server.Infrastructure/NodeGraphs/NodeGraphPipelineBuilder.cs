using PlServer.Domain.Nodes.Pipeline;
using PlServer.Server.Services;

namespace PlServer.Server.Infrastructure.NodeGraphs;

public class NodeGraphPipelineBuilder : INodeGraphPipelineBuilder
{
    public NodeGraphPipeline Build()
    {
        INodeGraphPipelineSource[] sources = [
            new DefaultNodeGraphSource()
        ];

        return new NodeGraphPipeline(sources);
    }
}
