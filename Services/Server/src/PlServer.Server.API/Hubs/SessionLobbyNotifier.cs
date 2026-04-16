using Microsoft.AspNetCore.SignalR;
using PlServer.Server.Infrastructure.Handlers.Sessions;

namespace PlServer.Server.API.Hubs;

public class SessionLobbyNotifier : ILobbyNotifier
{
    private readonly IHubContext<LobbyHub, ILobbyHubClient> _context;

    public SessionLobbyNotifier(IHubContext<LobbyHub, ILobbyHubClient> context)
    {
        _context = context;
    }

    public Task NotifyLobbyChangedAsync()
    {
        return _context.Clients.All.LobbyChangedAsync();
    }
}
