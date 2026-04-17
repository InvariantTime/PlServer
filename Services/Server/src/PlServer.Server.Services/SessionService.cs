
using PlServer.Application;
using PlServer.Domain.Results;
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;
using PlServer.Server.Services.DTOs;
using PlServer.Server.Services.Repositories;

namespace PlServer.Server.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _repository;
    private readonly IEventDispatcher _dispatcher;

    public SessionService(ISessionRepository repository, IEventDispatcher dispatcher)
    {
        _repository = repository;
        _dispatcher = dispatcher;
    }

    public async Task<Result<SessionSummaryDTO>> CreateSessionAsync(string name, UserId host, int maxPlayers)
    {
        var session = Session.Create(new SessionCreationQuery
        {
            Name = name,
            HostId = host,
            Id = SessionId.New(),//TODO: add node graph
            MaxUsersCount = maxPlayers
        });

        var result = _repository.AddSession(session);

        if (result == false)
            return Result.Failure<SessionSummaryDTO>(ErrorTypes.Common, "Unable to add session");

        await _dispatcher.DispatchEntityEventsAsync(session);
       
        return Result.Success(new SessionSummaryDTO(session.Key, session.Name, session.Users.HostId, session.Users.MaxUserCount, session.Users.UserCount));
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
            .Select(x => new SessionSummaryDTO(x.Key, x.Name, x.Users.HostId, x.Users.MaxUserCount, x.Users.Users.Count));
    }
}
