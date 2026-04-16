using PlServer.Application;
using PlServer.Server.Domain.Events;

namespace PlServer.Server.Infrastructure.Handlers.Sessions;

public class SessionLobbyEventHandler : IDomainEventHandler<ISessionEvent>
{
    private readonly ILobbyNotifier _notifier;

    public SessionLobbyEventHandler(ILobbyNotifier notifier)
    {
        _notifier = notifier;
    }

    public Task HandleAsync(ISessionEvent @event)
    {
        return _notifier.NotifyLobbyChangedAsync();
    }
}