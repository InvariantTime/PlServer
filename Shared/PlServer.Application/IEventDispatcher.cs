using PlServer.Domain;

namespace PlServer.Application;

public interface IEventDispatcher
{
    Task DispatchEventAsync<T>(T @event, CancellationToken cancellation = default);

    Task DispatchEventAsync(IDomainEvent @event,  CancellationToken cancellation = default);
}
