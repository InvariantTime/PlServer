
using PlServer.Domain.Results;
using PlServer.Server.Domain;
using PlServer.Server.Services.Repositories;
using System.Collections.Concurrent;

namespace PlServer.Server.Infrastructure;

public class InMemorySessionRepository : ISessionRepository
{
    private readonly ConcurrentDictionary<SessionId, Session> _sessions;

    public IReadOnlyDictionary<SessionId, Session> Sessions => _sessions.AsReadOnly();

    public InMemorySessionRepository()
    {
        _sessions = new ConcurrentDictionary<SessionId, Session>();
    }

    public Result AddSession(Session session)
    {
        bool result = _sessions.TryAdd(session.Key, session);

        return Result.Check(result == true, ErrorTypes.Common, "Unable to add session");
    }

    public Result<Session> RemoveSession(SessionId sessionId)
    {
        bool result = _sessions.TryRemove(sessionId, out var session);

        if (result == true)
            return Result.Success(session!);

        return Result.Failure<Session>(ErrorTypes.Common, $"Unable to remove session {sessionId}");
    }
}