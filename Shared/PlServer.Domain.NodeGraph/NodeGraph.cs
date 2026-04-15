
using PlServer.Domain.Results;

namespace PlServer.Domain.Nodes;

public class NodeGraph : AggregateRoot<NodeGraphId>
{
    private readonly NodeGraphPipeline _pipeline;
    private readonly NodeGraphContext _context;

    public ICollection<Node> Nodes => _context.Nodes;

    public IReadOnlyCollection<NodeConnection> Connections => _context.Connections;

    public NodeGraph(NodeGraphId id, NodeGraphPipeline pipeline) : base(id)
    {
        _pipeline = pipeline;
        _context = new NodeGraphContext();
    }

    public UnitResult<NodeErrors> ApplyCommand<T>(T command) where T : class
    {
        return _pipeline.ApplyCommand(_context, command);
    }

    public void Rebuild()
    {
        _pipeline.Rebuild();
    }
}
