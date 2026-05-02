
using PlServer.Server.Domain;
using System.Collections.Concurrent;

namespace PlServer.Server.Infrastructure.Sessions;

public class SessionWatchdogTracker
{
    private readonly ConcurrentDictionary<SessionId, DateTime> _times = new();

    public IReadOnlyDictionary<SessionId, DateTime> Times => _times.AsReadOnly();

    public void RemoveSession(SessionId session)
    {
        _times.TryRemove(session, out _);
    }

    public void AddSession(SessionId session)
    {
        _times.TryAdd(session, DateTime.UtcNow);
    }
}
