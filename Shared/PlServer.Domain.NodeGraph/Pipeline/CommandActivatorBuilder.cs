
namespace PlServer.Domain.Nodes.Pipeline;

internal static class CommandActivatorBuilder
{
    public static IDictionary<Type, CommandActivator> Build(
        IEnumerable<INodeGraphHandler> handlers, 
        IEnumerable<INodeGraphPolicy> polices)
    {

    }
}
