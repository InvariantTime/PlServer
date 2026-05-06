using PlServer.Domain.Nodes;
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Infrastructure.NodeGraphs;

public interface INodeGraphProvider
{
    Task<SynchronizeSnapshot> SyncAsync(SessionId id, long version);

    Task ApplyCommandAsync(NodeGraphCommand command, SessionId id, UserId user);
}
