
namespace PlServer.Domain.Nodes;

public class NodeGraph
{
    private readonly NodeGraphPipeline _pipeline;
    private readonly NodeGraphContext _context;

    public ICollection<Node> Nodes => _context.Nodes;

    public IReadOnlyCollection<NodeConnection> Connections => _context.Connections;

    public NodeGraph(NodeGraphPipeline pipeline)
    {
        _pipeline = pipeline;
        _context = new NodeGraphContext();
    }

    public void ApplyCommand(INodeGraphCommand command)
    {
        _pipeline.ApplyCommand(_context, command);
    }

    public void Rebuild()
    {
        _pipeline.Rebuild();
    }
}
