
using PlServer.Domain;

namespace PlServer.Infrastructure.Events;

public class BlackListHandlerDescriptor : IEventHandlerDescriptor
{
    private readonly IEventHandlerDescriptor _base;
    private readonly List<Type> _blackList;

    public Type HandlerType => _base.HandlerType;

    public BlackListHandlerDescriptor(IEventHandlerDescriptor @base)
    {
        _base = @base;
        _blackList = new List<Type>();
    }

    public BlackListHandlerDescriptor AddBlackListed(Type type)
    {
        if (typeof(IDomainEvent).IsAssignableFrom(type) == false)
            throw new InvalidCastException($"{type.FullName} is not domain event");

        _blackList.Add(type);
        return this;
    }

    public BlackListHandlerDescriptor AddBlackListed<T>() where T : IDomainEvent
    {
        _blackList.Add(typeof(T));
        return this;
    }

    public bool IsSupporting(Type eventType)
    {
        if (_blackList.Contains(eventType) == true)
            return false;

        return _base.IsSupporting(eventType);
    }
}

public static class BlackListHandlerDescriptorExtensions
{
    public static BlackListHandlerDescriptor AddBlackListed(this IEventHandlerDescriptor descriptor, Type eventType)
    {
        var blackList = new BlackListHandlerDescriptor(descriptor);
        blackList.AddBlackListed(eventType);

        return blackList;
    }

    public static BlackListHandlerDescriptor AddBlackListed<T>(this IEventHandlerDescriptor descriptor) where T : IDomainEvent
    {
        var blackList = new BlackListHandlerDescriptor(descriptor);
        blackList.AddBlackListed<T>();

        return blackList;
    }
}