using PlServer.Domain.Nodes;
using PlServer.Server.Domain;

namespace PlServer.Server.Services;

public interface INodeGraphService
{
    Task CreateNodeGraphAsync(NodeGraphId id, SessionId sessionId);

    Task RemoveNodeGraphAsync(NodeGraphId id);

    Task ApplyCommandAsync(NodeGraphId id, object command);
}
