using PlServer.Domain.Results;
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;
using PlServer.Server.Services.Repositories;
using System.Collections.Concurrent;

namespace PlServer.Server.Infrastructure.Repositories;

public class InMemorySessionRepository : ISessionRepository
{
    private readonly ConcurrentDictionary<SessionId, Session> _sessions;
    private readonly ConcurrentDictionary<UserId, SessionId> _usersIndex;
    private readonly ConcurrentDictionary<UserId, SessionId> _hostsIndex;

    public InMemorySessionRepository()
    {
        _sessions = new ConcurrentDictionary<SessionId, Session>();
        _usersIndex = new ConcurrentDictionary<UserId, SessionId>();
        _hostsIndex = new ConcurrentDictionary<UserId, SessionId>();
    }

    public bool AddSession(Session session)
    {
        bool result = _sessions.TryAdd(session.Key, session);

        if (result == true)
        {
            if (session.Users.UserCount > 0)
                UpdateUsers(session);

            UpdateHosts(session);
        }

        return result;
    }

    public Session? RemoveSession(SessionId sessionId)
    {
        _sessions.TryRemove(sessionId, out var session);

        if (session != null)
        {
            UpdateHosts(session);
            UpdateUsers(session);
        }

        return session;
    }

    public Session? GetSessionById(SessionId sessionId)
    {
        _sessions.TryGetValue(sessionId, out var session);
        return session;
    }

    public void Update(Session session)
    {
        UpdateUsers(session);
    }

    public ICollection<Session> GetAll()
    {
        return _sessions.Values;
    }

    public SessionId? GetSessionByUser(UserId user)
    {
        bool result = _usersIndex.TryGetValue(user, out var session);

        if (result == false)
            return null;

        return session;
    }

    public SessionId? GetSessionByHost(UserId host)
    {
        bool result = _hostsIndex.TryGetValue(host, out var session);

        if (result == false)
            return null;

        return session;
    }

    private void UpdateUsers(Session session)
    {
        var users = session.Users.Users;

        var outdated = _usersIndex
            .Where(x => x.Value == session.Key && users.Contains(x.Key) == false)
            .Select(x => x.Key)
            .ToList();

        foreach (var user in outdated)
            _usersIndex.TryRemove(user, out _);

        foreach (var user in users)
            _usersIndex[user] = session.Key;
    }

    private void UpdateHosts(Session session)
    {
        if (_sessions.ContainsKey(session.Key) == true)
        {
            _hostsIndex.TryAdd(session.Users.HostId, session.Key);
            return;
        }

        _hostsIndex.TryRemove(session.Users.HostId, out _);
    }
}