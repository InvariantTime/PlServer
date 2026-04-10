namespace PlServer.Domain.Nodes;

public class NodeGraphPipeline
{
    private readonly Dictionary<Type, INodeGraphPolicy> _polices = new();

    public void Rebuild()
    {
        _polices.Clear();
    }

    public void ApplyCommand(NodeGraphContext context, INodeGraphCommand command)
    {
        var result = _polices.TryGetValue(command.GetType(), out var policy);

        if (result == true)
        {
            policy!.Validate();
        }

        command.Execute();
    }
}
