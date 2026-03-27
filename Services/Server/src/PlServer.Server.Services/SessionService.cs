
using PlServer.Domain.Results;
using PlServer.Server.Domain;

namespace PlServer.Server.Services;

public class SessionService : ISessionService
{
    public async Task<Session> CreateSessionAsync(string name, UserId user, int maxPlayers)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteSessionAsync(SessionId session)
    {
        throw new NotImplementedException();
    }

    public Task<Result> JoinAsync(SessionId session, UserId user)
    {
        throw new NotImplementedException();
    }

    public Task<Result> LeaveAsync(SessionId session, UserId user)
    {
        throw new NotImplementedException();
    }
}
