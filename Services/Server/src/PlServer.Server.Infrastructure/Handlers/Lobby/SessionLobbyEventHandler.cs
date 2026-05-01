using PlServer.Application;
using PlServer.Server.Domain.Events;

namespace PlServer.Server.Infrastructure.Handlers.Lobby;

public class SessionLobbyEventHandler : IDomainEventHandler<ISessionEvent>
{
    private readonly ILobbyNotifier _notifier;

    public SessionLobbyEventHandler(ILobbyNotifier notifier)
    {
        _notifier = notifier;
    }

    public Task HandleAsync(ISessionEvent @event, CancellationToken token)
    {
        return _notifier.NotifyLobbyChangedAsync();
    }
}