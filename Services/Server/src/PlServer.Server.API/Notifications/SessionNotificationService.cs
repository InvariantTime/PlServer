using Microsoft.AspNetCore.SignalR;
using PlServer.Server.API.Hubs;
using PlServer.Server.Services.Sessions;

namespace PlServer.Server.API.Notifications;

public class SessionNotificationService : ISessionNotificationService
{
    private readonly IHubContext<SessionHub, ISessionClient> _context;

    public SessionNotificationService(IHubContext<SessionHub, ISessionClient> context)
    {
        _context = context;
    }

    public Task NotifyLobbyChangedAsync(IEnumerable<Session> sessions, CancellationToken cancellation)
    {
        return _context.Clients.All.OnSessionListChangedAsync(sessions.Select(x => new Responces.SessionResponce(x.Name, x.Id)));
    }
}