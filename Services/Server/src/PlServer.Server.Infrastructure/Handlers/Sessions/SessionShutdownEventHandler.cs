
using Microsoft.Extensions.Logging;
using PlServer.Application;
using PlServer.Server.Domain.Events;
using PlServer.Server.Infrastructure.Sessions;

namespace PlServer.Server.Infrastructure.Handlers.Sessions;

public class SessionShutdownEventHandler : IDomainEventHandler<SessionClosedEvent>
{
    private readonly ISessionNotifier _notifier;
    private readonly ILogger<SessionShutdownEventHandler> _logger;

    public SessionShutdownEventHandler(ISessionNotifier notifier, ILogger<SessionShutdownEventHandler> logger)
    {
        _notifier = notifier;
        _logger = logger;
    }

    public Task HandleAsync(SessionClosedEvent @event, CancellationToken cancellation)
    {
        _logger.LogDebug($"session {{{@event.SessionId}}} [{@event.Name}] closed");
        return _notifier.HandleShutdownAsync(@event.SessionId);
    }
}