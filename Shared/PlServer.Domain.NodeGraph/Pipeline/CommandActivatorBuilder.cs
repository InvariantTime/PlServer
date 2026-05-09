
namespace PlServer.Domain.Nodes.Pipeline;

internal static class CommandActivatorBuilder
{
    public static IDictionary<Type, CommandActivator> Build(
        IEnumerable<INodeGraphHandler> handlers, 
        IEnumerable<INodeGraphPolicy> polices)
    {
        Dictionary<Type, CommandActivator> activators = new();

        foreach (var handler in handlers)
        {
            var activator = CreateActivator(handler, polices);
            activators.Add(handler.CommandType, activator);
        }

        return activators;
    }

    private static CommandActivator CreateActivator(INodeGraphHandler handler, IEnumerable<INodeGraphPolicy> polices)
    {
        var matchPolices = polices
            .Where(x => x.CommandType == handler.CommandType)
            .ToArray();

        return (object command, NodeGraphContext context) =>
        {
            foreach (var policy in matchPolices)
            {
                var result = policy.Validate(context, command);

                if (result.IsSuccess == false)
                    return result;
            }

            return handler.Handle(command, context);
        };
    }
}
