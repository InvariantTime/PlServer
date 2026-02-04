
using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace PlServer.Server.Infrastructure.Sessions;

public class SessionService : ISessionService
{
    private readonly ConcurrentDictionary<Guid, Session> _sessions;

    public ICollection<Session> Sessions => _sessions.Values;

    public SessionService()
    {
        _sessions = new();
    }


    public Session CreateSession(string name)
    {
        var id = Guid.NewGuid();

        while (_sessions.ContainsKey(id) == true)
            id = Guid.NewGuid();

        var session = new Session(name, id);
        _sessions.TryAdd(id, session);

        return session;
    }

    public Session? GetSession(Guid id)
    {
        throw new NotImplementedException();
    }
}
