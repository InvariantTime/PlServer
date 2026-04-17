using PlServer.Domain;

namespace PlServer.Application;

public interface IEventDispatcher
{
    Task DispatchEventAsync(IDomainEvent @event,  CancellationToken cancellation = default);
}
