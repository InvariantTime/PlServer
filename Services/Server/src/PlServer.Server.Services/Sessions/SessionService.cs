
using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace PlServer.Server.Services.Sessions;

public class SessionService : ISessionService
{
    private readonly ConcurrentDictionary<Guid, Session> _sessions;
    private readonly ISessionNotificationService _notification;

    public ICollection<Session> Sessions => _sessions.Values;

    public SessionService(ISessionNotificationService notification)
    {
        _sessions = new();
        _notification = notification;
    }


    public async Task<Session?> CreateSessionAsync(string name)
    {
        var id = Guid.NewGuid();

        while (_sessions.ContainsKey(id) == true)
            id = Guid.NewGuid();

        var session = new Session(name, id);
        bool result = _sessions.TryAdd(id, session);

        if (result == false)
            return null;

        await _notification.NotifyLobbyChangedAsync(_sessions.Values, CancellationToken.None);

        return session;
    }

    public Session? GetSession(Guid id)
    {
        throw new NotImplementedException();
    }
}
