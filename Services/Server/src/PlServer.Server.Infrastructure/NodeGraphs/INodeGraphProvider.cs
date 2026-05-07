using PlServer.Domain.Nodes;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Infrastructure.NodeGraphs;

public interface INodeGraphProvider
{
    Task<SynchronizeSnapshot> SyncAsync(NodeGraphId id, long version);

    Task ApplyCommandAsync(NodeGraphCommand command, NodeGraphId id, UserId user);
}
