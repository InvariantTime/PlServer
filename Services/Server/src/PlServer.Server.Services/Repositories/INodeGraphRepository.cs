
using PlServer.Domain.Nodes;

namespace PlServer.Server.Services.Repositories;

public interface INodeGraphRepository
{
    NodeGraph? GetNodeGraphById(NodeGraphId id);

    bool AddNodeGraph(NodeGraph graph);

    bool RemoveNodeGraph(NodeGraphId id);

    ICollection<NodeGraph> GetAll();
}