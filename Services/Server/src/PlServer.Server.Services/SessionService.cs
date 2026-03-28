
using PlServer.Domain.Results;
using PlServer.Server.Domain;
using PlServer.Server.Services.DTOs;

namespace PlServer.Server.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _repository;

    public SessionService(ISessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Session>> CreateSessionAsync(string name, UserId user, int maxPlayers)
    {
        var session = Session.Create(new SessionCreationQuery
        {
            Name = name,
            HostId = user,
            Id = SessionId.New(),
            MaxUsersCount = maxPlayers
        });

        var result = _repository.AddSession(session);

        if (result.IsSuccess == false)
            return Result.Failure<Session>(result.Error);

        

        return Result.Success(session);
    }

    public async Task<Result> DeleteSessionAsync(SessionId sessionId)
    {
        _repository.Sessions.TryGetValue(sessionId, out var session);

        if (session == null)
            return Result.Failure(ErrorTypes.Common, $"There is no session with id {sessionId}");

        //session.Shutdown();

        return Result.Success();
    }

    public async Task<Result> JoinAsync(SessionId sessionId, UserId user)
    {
        _repository.Sessions.TryGetValue(sessionId, out var session);

        if (session == null)
            return Result.Failure(ErrorTypes.Common);

        return Result.Success();
    }

    public async Task<Result> LeaveAsync(SessionId sessionId, UserId user)
    {
        _repository.Sessions.TryGetValue(sessionId, out var session);

        if (session == null)
            return Result.Failure(ErrorTypes.Common);

        return Result.Success();
    }

    public IEnumerable<SessionSummaryDTO> GetSessionSummaryDTOs()
    {
        return _repository.Sessions.Values
            .Select(x => new SessionSummaryDTO(x.Key, x.Name, x.HostId, x.MaxUsersCount, x.Users.Count));
    }
}
