using PlServer.Domain.Nodes;

namespace PlServer.Server.Services;

public interface INodeGraphPipelineBuilder
{
    NodeGraphPipeline Build();
}
