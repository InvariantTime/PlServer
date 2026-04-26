using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;
using System.Collections.Concurrent;

namespace PlServer.Server.Infrastructure.Sessions;

public class SessionConnectionTracker : ISessionConnectionTracker
{
    private readonly ConcurrentDictionary<string, SessionConnection> _connections = new();

    public SessionConnection CreateConnection(string id, SessionId session, UserId user)
    {
        var connection = new SessionConnection(session, user);
        _connections.TryAdd(id, connection);

        return connection;
    }

    public ICollection<SessionConnection> GetAll(SessionId session)
    {
        return _connections.Values;
    }

    public SessionConnection? GetConnection(string id)
    {
        _connections.TryGetValue(id, out var connection);
        return connection;
    }

    public SessionConnection? RemoveConnection(string id)
    {
        _connections.TryRemove(id, out var connection);
        return connection;
    }
}
