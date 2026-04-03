using PlServer.Domain;

namespace PlServer.Application;

public interface IEventDispatcher
{
    Task DispatchEventAsync<T>(IDomainEvent @event, CancellationToken cancellation = default) where T : struct;
}
