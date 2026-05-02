using Microsoft.Extensions.DependencyInjection;
using PlServer.Application;
using PlServer.Domain;

namespace PlServer.Infrastructure.Events;

public static class EventDispatcherExtensions
{
    public static void AddEventDispatching(this IServiceCollection services, Action<DispatcherFactoryBuilder> initAction)
    {
        var builder = new DispatcherFactoryBuilder();
        initAction.Invoke(builder);

        foreach (var descriptor in builder.Descriptors)
            services.AddScoped(descriptor.HandlerType);

        var factory = builder.Build();
        services.AddSingleton(factory);

        services.AddScoped(scope =>
        {
            var factory = scope.GetRequiredService<IEventDispatcherFactory>();
            return factory.CreateDispatcher(scope);
        });
    }

    public static GenericHandlerDescriptor AddGenericHandler<TEvent, THandler>(this DispatcherFactoryBuilder builder) 
        where TEvent : IDomainEvent
        where THandler : IDomainEventHandler<TEvent>
    {
        var descriptor = new GenericHandlerDescriptor(typeof(THandler), typeof(TEvent));
        builder.AddDescriptor(descriptor);

        return descriptor;
    }

    public static ConcreteTypedHandlerDescriptor AddHandler<TEvent, THandler>(this DispatcherFactoryBuilder builder)
        where TEvent : IDomainEvent
        where THandler : IDomainEventHandler<TEvent>
    {
        var descriptor = new ConcreteTypedHandlerDescriptor(typeof(THandler), typeof(TEvent));
        builder.AddDescriptor(descriptor);

        return descriptor;
    }

    public static ManyTypedHandlerDescriptor AddMultipleHandler<T>() where T : class
    {
        var handlerType = typeof(T);
        var valid = handlerType.GetInterfaces()
            .FirstOrDefault(x => x.IsGenericType == true 
                && x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>));

        if (valid == null)
            throw new InvalidCastException($"{handlerType.FullName} is not event handler");

        var descriptor = new ManyTypedHandlerDescriptor(handlerType);
        return descriptor;
    }
}
