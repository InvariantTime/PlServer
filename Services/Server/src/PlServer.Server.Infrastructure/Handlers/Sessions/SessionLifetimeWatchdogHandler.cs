using PlServer.Application;
using PlServer.Server.Domain.Events;
using PlServer.Server.Infrastructure.Sessions;

namespace PlServer.Server.Infrastructure.Handlers.Sessions;

public class SessionLifetimeWatchdogHandler : IDomainEventHandler<SessionCreatedEvent>, IDomainEventHandler<SessionConfirmedEvent>
{
    private readonly SessionWatchdogTracker _tracker;

    public SessionLifetimeWatchdogHandler(SessionWatchdogTracker tracker)
    {
        _tracker = tracker;
    }

    public Task HandleAsync(SessionCreatedEvent @event, CancellationToken cancellation)
    {
        _tracker.AddSession(@event.SessionId);
        return Task.CompletedTask;
    }

    public Task HandleAsync(SessionConfirmedEvent @event, CancellationToken cancellation)
    {
        _tracker.RemoveSession(@event.SessionId);
        return Task.CompletedTask;
    }
}
