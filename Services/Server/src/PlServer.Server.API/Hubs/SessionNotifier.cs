using Microsoft.AspNetCore.SignalR;
using PlServer.Server.Domain;
using PlServer.Server.Infrastructure.Sessions;

namespace PlServer.Server.API.Hubs;

public class SessionNotifier : ISessionNotifier
{
    private readonly IHubContext<SessionHub, ISessionClient> _context;
    private readonly ISessionConnectionTracker _tracker;

    public SessionNotifier(IHubContext<SessionHub, ISessionClient> context, ISessionConnectionTracker tracker)
    {
        _context = context;
        _tracker = tracker;
    }

    public async Task HandleShutdownAsync(SessionId session)
    {
        var connections = _tracker.GetAll(session);

        foreach (var connection in connections)
            await _context.Clients.Client(connection.Connection).ShutdownAsync("host closed the session");

        _tracker.Clear(session);
    }
}
