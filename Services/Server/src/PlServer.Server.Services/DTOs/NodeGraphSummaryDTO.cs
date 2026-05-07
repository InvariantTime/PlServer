
using PlServer.Domain.Nodes;

namespace PlServer.Server.Services.DTOs;

public record NodeGraphSummaryDTO
{
    public long Version { get; }

    public NodeGraphId Id { get; }

    public ICollection<NodeConnection> Connections { get; }

    public ICollection<NodeSummaryDTO> Nodes { get; }

    private NodeGraphSummaryDTO(ICollection<NodeConnection> connections, ICollection<NodeSummaryDTO> nodes, long version, NodeGraphId id)
    {
        Version = version;
        Id = id;
        Connections = connections;
        Nodes = nodes;
    }

    public static NodeGraphSummaryDTO Create(NodeGraph graph)
    {
        return new NodeGraphSummaryDTO(graph.Connections.ToArray(), [], graph.Version, graph.Key);//TODO: nodes
    }
}
