using PlServer.Domain;

namespace PlServer.Application;

public delegate Task EventHandlerLauncher(IDomainEvent @event, CancellationToken cancellation = default);