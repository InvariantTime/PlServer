
namespace PlServer.Domain.Nodes;

public class NodeGraph
{
    private readonly NodeGraphPipeline _pipeline;
    private readonly NodeGraphContext _context;

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
