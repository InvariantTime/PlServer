using PlServer.Domain.Results;
using PlServer.Server.Domain;

namespace PlServer.Server.Services.Repositories;

public interface ISessionRepository
{
    IReadOnlyDictionary<SessionId, Session> Sessions { get; }

    Result AddSession(Session session);

    Result<Session> RemoveSession(SessionId session);
}
