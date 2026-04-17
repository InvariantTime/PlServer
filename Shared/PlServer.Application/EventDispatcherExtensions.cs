using PlServer.Domain;

namespace PlServer.Application;

public static class EventDispatcherExtensions
{
    public static async Task DispatchEntityEventsAsync<T>(this IEventDispatcher dispatcher, IEventSource<T> source) where T : IDomainEvent
    {
        foreach (var @event in source.Events)
            await dispatcher.DispatchEventAsync(@event);
    }
}
