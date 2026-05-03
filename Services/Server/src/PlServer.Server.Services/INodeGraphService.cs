using PlServer.Domain.Nodes;

namespace PlServer.Server.Services;

public interface INodeGraphService
{
    Task CreateNodeGraphAsync(NodeGraphId id);

    Task RemoveNodeGraphAsync(NodeGraphId id);

    Task ApplyCommandAsync(NodeGraphId id, object command);
}
