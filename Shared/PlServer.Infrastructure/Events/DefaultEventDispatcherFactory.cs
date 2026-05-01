
using Microsoft.Extensions.DependencyInjection;
using PlServer.Application;
using PlServer.Domain;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;

namespace PlServer.Infrastructure.Events;

internal delegate Task HandlerCaller(IDomainEvent @event, object handler, CancellationToken cancellation);

internal class DefaultEventDispatcherFactory : IEventDispatcherFactory, IEventLauncherSource
{
    private static readonly EventHandlerLauncher _emptyLauncher = (_, _, _) => Task.CompletedTask;

    private readonly ConcurrentDictionary<Type, HandlerCaller> _cachedHandlers;
    private readonly ConcurrentDictionary<Type, EventHandlerLauncher> _cachedLaunchers;
    private readonly ImmutableArray<IEventHandlerDescriptor> _descriptors;

    public DefaultEventDispatcherFactory(IEnumerable<IEventHandlerDescriptor> descriptors)
    {
        _cachedLaunchers = new();
        _cachedHandlers = new();
        _descriptors = descriptors.ToImmutableArray();
    }

    public IEventDispatcher CreateDispatcher(IServiceProvider scope)
    {
        return new EventDispatcher(this, scope);
    }

    public EventHandlerLauncher GetOrCreateLauncher(Type eventType)
    {
        return _cachedLaunchers.GetOrAdd(eventType, CreateLauncher);
    }

    private EventHandlerLauncher CreateLauncher(Type eventType)
    {
        var handlerTypes = _descriptors
            .Where(x => x.IsSupporting(eventType) == true)
            .Select(x => x.HandlerType);

        if (handlerTypes.Any() == false)
            return _emptyLauncher;

        var handlers = handlerTypes
            .Select(x => (Type: x, Caller: _cachedHandlers.GetOrAdd(x, CreateCaller)))
            .ToArray();

        return async (@event, scope, cancellation) =>
        {
            foreach (var caller in handlers)
            {
                var handler = scope.GetRequiredService(caller.Type);
                await caller.Caller.Invoke(@event, handler, cancellation);
            }
        };
    }

    private HandlerCaller CreateCaller(Type handlerType)
    {
        var handlerParameter = Expression.Parameter(typeof(object));
        var eventParameter = Expression.Parameter(typeof(IDomainEvent));
        var cancellationParameter = Expression.Parameter(typeof(CancellationToken));

        var interfaces = handlerType.GetInterfaces();

        var genericHandler = interfaces
            .First(x => x.IsGenericType == true && x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>));

        var generic = genericHandler.GetGenericArguments().First();
        var method = genericHandler.GetMethod("HandleAsync", BindingFlags.Public | BindingFlags.Instance)!;

        var castedHandler = Expression.Convert(handlerParameter, genericHandler);
        var castedEvent = Expression.Convert(eventParameter, generic);

        var body = Expression.Call(castedHandler, method, castedEvent, cancellationParameter);
        var lambda = Expression.Lambda<HandlerCaller>(body, eventParameter, handlerParameter, cancellationParameter);

        return lambda.Compile();
    }
}
