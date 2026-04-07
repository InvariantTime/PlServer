
namespace PlServer.Domain.Nodes;

public class NodeGraph
{
    private readonly NodeGraphPipeline _pipeline;

    public NodeGraph(NodeGraphPipeline pipeline)
    {
        _pipeline = pipeline;
    }

    public void ApplyCommand(INodeGraphCommand command)
    {

    }

    public void Rebuild()
    {
        _pipeline.Rebuild();
    }
}
