using PlServer.Domain.Nodes.Pipeline;

namespace PlServer.Server.Services;

public interface INodeGraphPipelineBuilder
{
    NodeGraphPipeline Build();
}
