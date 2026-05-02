using PlServer.Domain;

namespace PlServer.Infrastructure.Events;

public class ManyTypedHandlerDescriptor : IEventHandlerDescriptor
{
    private static EqualityComparer<Type> _comparer = 
        EqualityComparer<Type>.Create((left, right) => left?.IsAssignableFrom(right) == true);

    private readonly List<Type> _eventTypes;

    public Type HandlerType { get; }

    public ManyTypedHandlerDescriptor(Type handlerType)
    {
        HandlerType = handlerType;
        _eventTypes = new List<Type>();
    }

    public ManyTypedHandlerDescriptor AddEventType(Type type)
    {
        if (typeof(IDomainEvent).IsAssignableFrom(type) == false)
            throw new InvalidCastException($"{type.FullName} is not domain event");

        _eventTypes.Add(type);
        return this;
    }

    public ManyTypedHandlerDescriptor AddEventType<T>() where T : IDomainEvent
    {
        _eventTypes.Add(typeof(T));
        return this;
    }

    public bool IsSupporting(Type eventType)
    {
        var result = _eventTypes.Contains(eventType, _comparer);
        return result;
    }
}
