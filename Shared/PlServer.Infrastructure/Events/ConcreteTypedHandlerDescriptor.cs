
namespace PlServer.Infrastructure.Events;

public class ConcreteTypedHandlerDescriptor : IEventHandlerDescriptor
{
    public Type HandlerType { get; }

    public Type EventType { get; }

    public ConcreteTypedHandlerDescriptor(Type handlerType, Type eventType)
    {
        HandlerType = handlerType;
        EventType = eventType;
    }

    public bool IsSupporting(Type eventType)
    {
        return eventType == EventType;
    }
}
