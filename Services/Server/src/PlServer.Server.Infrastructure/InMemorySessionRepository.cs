
using PlServer.Domain.Results;
using PlServer.Server.Domain;
using PlServer.Server.Services.Repositories;
using System.Collections.Concurrent;

namespace PlServer.Server.Infrastructure;

public class InMemorySessionRepository : ISessionRepository
{
    private readonly ConcurrentDictionary<SessionId, Session> _sessions;

    public InMemorySessionRepository()
    {
        _sessions = new ConcurrentDictionary<SessionId, Session>();
    }

    public bool AddSession(Session session)
    {
        bool result = _sessions.TryAdd(session.Key, session);
        return result;
    }

    public Session? RemoveSession(SessionId sessionId)
    {
        _sessions.TryRemove(sessionId, out var session);
        return session;
    }

    public Session? GetSessionById(SessionId sessionId)
    {
        _sessions.TryGetValue(sessionId, out var session);
        return session;
    }

    public ICollection<Session> GetAll()
    {
        return _sessions.Values;
    }
}