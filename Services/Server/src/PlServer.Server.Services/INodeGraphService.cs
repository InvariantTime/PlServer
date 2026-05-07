using PlServer.Domain.Nodes;
using PlServer.Server.Domain;
using PlServer.Server.Services.DTOs;

namespace PlServer.Server.Services;

public interface INodeGraphService
{
    Task CreateNodeGraphAsync(NodeGraphId id, SessionId sessionId);

    Task RemoveNodeGraphAsync(NodeGraphId id);

    Task ApplyCommandAsync(NodeGraphId id, object command);

    NodeGraphSummaryDTO? GetNodeGraphDto(NodeGraphId id);
}
