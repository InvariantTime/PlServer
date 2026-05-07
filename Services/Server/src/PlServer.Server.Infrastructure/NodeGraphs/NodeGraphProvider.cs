
using PlServer.Domain.Nodes;
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;
using PlServer.Server.Services;

namespace PlServer.Server.Infrastructure.NodeGraphs;

public class NodeGraphProvider : INodeGraphProvider
{
    private readonly INodeGraphService _service;

    public NodeGraphProvider(INodeGraphService service)
    {
        _service = service;
    }

    public Task ApplyCommandAsync(NodeGraphCommand command, NodeGraphId id, UserId user)
    {
        return _service.ApplyCommandAsync(id, command);
    }

    public Task<SynchronizeSnapshot> SyncAsync(NodeGraphId id, long version)
    {
        var dto = _service.GetNodeGraphDto(id);

        if (dto == null)
            return Task.FromResult(SynchronizeSnapshot.Empty);

        var snapshot = SynchronizeSnapshot.CreateFullSync(dto.Nodes, dto.Connections, dto.Version);
        return Task.FromResult<SynchronizeSnapshot>(snapshot);
    }
}