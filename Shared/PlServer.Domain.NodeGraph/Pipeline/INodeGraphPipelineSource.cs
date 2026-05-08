
namespace PlServer.Domain.Nodes.Pipeline;

public interface INodeGraphPipelineSource
{
    IEnumerable<INodeGraphPolicy> GetPolicies();

    IEnumerable<INodeGraphHandler> GetHandlers();
}
