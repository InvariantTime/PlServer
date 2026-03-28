
using PlServer.Domain.Results;
using PlServer.Server.Domain;
using PlServer.Server.Services.DTOs;
using PlServer.Server.Services.Repositories;

namespace PlServer.Server.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _repository;

    public SessionService(ISessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<SessionSummaryDTO>> CreateSessionAsync(string name, UserId host, int maxPlayers)
    {
        var session = Session.Create(new SessionCreationQuery
        {
            Name = name,
            HostId = host,
            Id = SessionId.New(),
            MaxUsersCount = maxPlayers
        });

        var result = _repository.AddSession(session);

        if (result == false)
            return Result.Failure<SessionSummaryDTO>(ErrorTypes.Common, "Unable to add session");

        //TODO: dispatch events
       
        return Result.Success(new SessionSummaryDTO(session.Key, session.Name, session.HostId, session.MaxUsersCount, session.Users.Count));
    }

    public async Task<Result> DeleteSessionAsync(SessionId sessionId)
    {
        var session = _repository.GetSessionById(sessionId);

        if (session == null)
            return Result.Failure(ErrorTypes.Common, $"There is no session with id {sessionId}");

        //session.Shutdown();

        return Result.Success();
    }

    public async Task<Result> JoinAsync(SessionId sessionId, UserId user)
    {
        var session = _repository.GetSessionById(sessionId);

        if (session == null)
            return Result.Failure(ErrorTypes.Common);

        return Result.Success();
    }

    public async Task<Result> LeaveAsync(SessionId sessionId, UserId user)
    {
        var session = _repository.GetSessionById(sessionId);

        if (session == null)
            return Result.Failure(ErrorTypes.Common);

        return Result.Success();
    }

    public IEnumerable<SessionSummaryDTO> GetSessionSummaryDtos()
    {
        return _repository.GetAll()
            .Select(x => new SessionSummaryDTO(x.Key, x.Name, x.HostId, x.MaxUsersCount, x.Users.Count));
    }
}
