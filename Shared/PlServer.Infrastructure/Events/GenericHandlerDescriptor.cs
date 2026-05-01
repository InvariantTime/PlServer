namespace PlServer.Infrastructure.Events;

public class GenericHandlerDescriptor : IEventHandlerDescriptor
{
    public Type HandlerType { get; }

    public Type GenericType { get; }

    public GenericHandlerDescriptor(Type handler, Type generic)
    {
        HandlerType = handler;
        GenericType = generic;
    }

    public bool IsSupporting(Type eventType)
    {
        return GenericType.IsAssignableFrom(eventType);
    }
}
