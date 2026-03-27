using PlServer.Domain;

namespace PlServer.Application;

public interface IDomainEventHandler<T> where T : IDomainEvent
{
    Task HandleAsync(T @event);
}
