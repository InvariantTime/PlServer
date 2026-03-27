using PlServer.Domain.Results;
using PlServer.Server.Domain;

namespace PlServer.Server.Services;

public interface ISessionService
{
    Task<Session> CreateSessionAsync(string name, UserId user, int maxPlayers);

    Task<Result> JoinAsync(SessionId session, UserId user);

    Task<Result> LeaveAsync(SessionId session, UserId user);

    Task<Result> DeleteSessionAsync(SessionId session);
}