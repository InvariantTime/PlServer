using PlServer.Domain.Results;
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;
using PlServer.Server.Services.DTOs;

namespace PlServer.Server.Services;

public interface ISessionService
{
    IEnumerable<SessionSummaryDTO> GetSessionSummaryDtos();

    Task<Result<SessionSummaryDTO>> CreateSessionAsync(string name, UserId host, int maxPlayers);

    Task<Result> JoinAsync(SessionId session, UserId user);

    Task<Result> LeaveAsync(SessionId session, UserId user);

    Task<Result> DeleteSessionAsync(SessionId session);
}