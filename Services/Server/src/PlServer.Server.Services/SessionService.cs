using PlServer.Application;
using PlServer.Domain.Nodes;
using PlServer.Domain.Results;
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;
using PlServer.Server.Services.DTOs;
using PlServer.Server.Services.Repositories;

namespace PlServer.Server.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _repository;
    private readonly INodeGraphService _nodeGraphs;
    private readonly IEventDispatcher _dispatcher;

    public SessionService(ISessionRepository repository, INodeGraphService nodeGraphs, IEventDispatcher dispatcher)
    {
        _repository = repository;
        _dispatcher = dispatcher;
        _nodeGraphs = nodeGraphs;
    }

    public async Task<Result<SessionSummaryDTO>> CreateSessionAsync(string name, UserId host, int maxPlayers)
    {
        if (_repository.CanCreateSession(host) == false)
            return Result.Failure<SessionSummaryDTO>(ErrorTypes.Common, "User is already in a session");

        var nodeGraphId = NodeGraphId.New();

        var session = Session.Create(new SessionCreationQuery
        {
            Name = name,
            HostId = host,
            Id = SessionId.New(),
            GraphId = nodeGraphId,
            MaxUsersCount = maxPlayers
        });

        var result = _repository.AddSession(session);

        if (result == false)
            return Result.Failure<SessionSummaryDTO>(ErrorTypes.Common, "Unable to add session");

        await _nodeGraphs.CreateNodeGraphAsync(nodeGraphId);

        await _dispatcher.DispatchEntityEventsAsync(session);
       
        return Result.Success(new SessionSummaryDTO(session.Key, session.GraphId, session.Name, session.Users));
    }

    public async Task<Result> DeleteSessionAsync(SessionId sessionId)
    {
        var session = _repository.GetSessionById(sessionId);

        if (session == null)
            return Result.Failure(ErrorTypes.Common, $"There is no session with id {sessionId}");

        session.Shutdown();

        if (session.State == SessionStates.Shutdown)
            await RemoveSessionAsync(sessionId);

        await _dispatcher.DispatchEntityEventsAsync(session);

        return Result.Success();
    }

    public async Task<UnitResult<SessionErrors>> JoinAsync(SessionId sessionId, UserId user)
    {
        if (_repository.CanJoinTo(user, sessionId) == false)
            return Result.Failure(SessionErrors.UserAlreadyExists, "User is already in a session");

        var session = _repository.GetSessionById(sessionId);

        if (session == null)
            return Result.Failure(SessionErrors.Common, "There is no such session");

        var result = session.JoinPlayer(user);

        if (result.IsSuccess == false)
            return result;

        _repository.Update(session);
        await _dispatcher.DispatchEntityEventsAsync(session);

        return Result.Success<SessionErrors>();
    }

    public async Task<UnitResult<SessionErrors>> LeaveAsync(SessionId sessionId, UserId user)
    {
        var session = _repository.GetSessionById(sessionId);

        if (session == null)
            return Result.Failure(SessionErrors.Common, "There is no such session");

        var result = session.LeavePlayer(user);

        if (result.IsSuccess == false)
            return result;

        if (session.State == SessionStates.Shutdown)
        {
            await RemoveSessionAsync(sessionId);
        }
        else
        {
            _repository.Update(session);
        }

        await _dispatcher.DispatchEntityEventsAsync(session);
        return Result.Success<SessionErrors>();
    }

    public IEnumerable<SessionSummaryDTO> GetSessionSummaryDtos()
    {
        return _repository.GetAll()
            .Where(x => x.State != SessionStates.Pending && x.State != SessionStates.Shutdown)
            .Where(x => x.Users.MaxUserCount > 1)
            .Select(x => new SessionSummaryDTO(x.Key, x.GraphId, x.Name, x.Users));
    }

    private async Task RemoveSessionAsync(SessionId sessionId)
    {
        var session = _repository.RemoveSession(sessionId);

        if (session != null)
            await _nodeGraphs.RemoveNodeGraphAsync(session.GraphId);
    }
}