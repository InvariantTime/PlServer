
namespace PlServer.Domain.Nodes;

public interface INodeGraph
{
    IEnumerable<Node> Nodes { get; }

    IEnumerable<NodeConnection> Connections { get; }
}
