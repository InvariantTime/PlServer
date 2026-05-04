
using PlServer.Domain.Nodes;
using PlServer.Server.Domain;

namespace PlServer.Server.Services.Repositories;

public interface INodeGraphRepository
{
    NodeGraphFacade? GetNodeGraphById(NodeGraphId id);

    bool AddNodeGraph(NodeGraphFacade facade);

    bool RemoveNodeGraph(NodeGraphId id);

    ICollection<NodeGraphFacade> GetAll();

    NodeGraphFacade? GetBySessionId(SessionId session);
}