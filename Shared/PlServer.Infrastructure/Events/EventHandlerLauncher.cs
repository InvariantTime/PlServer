using PlServer.Domain;

namespace PlServer.Infrastructure.Events;

public delegate Task EventHandlerLauncher(IDomainEvent @event, IServiceProvider scope, CancellationToken cancellation);

public interface IEventLauncherSource
{
    EventHandlerLauncher GetOrCreateLauncher(Type eventType);
}
