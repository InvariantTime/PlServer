
using PlServer.Domain.Nodes;
using PlServer.Domain.Nodes.Pipeline;
using PlServer.Server.Infrastructure.NodeGraphs.Handlers;

namespace PlServer.Server.Infrastructure.NodeGraphs;

public class DefaultNodeGraphSource : INodeGraphPipelineSource
{
    private readonly INodeGraphHandler[] _handlers = [
        new AddNodeHandler(),
        new RemoveNodeHandler()
    ];

    public IEnumerable<INodeGraphHandler> GetHandlers()
    {
        return _handlers;
    }

    public IEnumerable<INodeGraphPolicy> GetPolicies()
    {
        return [];
    }
}
