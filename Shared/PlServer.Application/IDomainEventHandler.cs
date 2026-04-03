using PlServer.Domain;

namespace PlServer.Application;

public interface IDomainEventHandler<T> where T : IDomainEvent
{
    void Handle(T @event);
}
