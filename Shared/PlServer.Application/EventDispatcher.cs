using PlServer.Domain;
using System.Collections.Immutable;

namespace PlServer.Application;

public class EventDispatcher : IEventDispatcher
{
    private readonly ImmutableDictionary<Type, EventHandlerLauncher> _launchers;

    public EventDispatcher(IDictionary<Type, EventHandlerLauncher> handlers)
    {
        _launchers = handlers.ToImmutableDictionary();
    }

    public Task DispatchEventAsync(IDomainEvent @event, CancellationToken cancellation = default)
    {
        var result = _launchers.TryGetValue(@event.GetType(), out var launcher);

        if (result == false)
            return Task.CompletedTask;

        return launcher?.Invoke(@event, cancellation) ?? Task.CompletedTask;
    }
}