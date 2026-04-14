
using PlServer.Domain;
using PlServer.Domain.Nodes;
using PlServer.Domain.Results;
using PlServer.Server.Domain.Events;

namespace PlServer.Server.Domain;

public class Session : AggregateRoot<SessionId>
{
    private readonly HashSet<UserId> _users;
    private readonly NodeGraph _graph;

    public UserId HostId { get; }

    public string Name { get; }

    public IReadOnlyCollection<UserId> Users => _users.AsReadOnly();

    public int MaxUsersCount { get; }

    private Session(SessionId id, string name, int maxUsersCount, UserId hostId) : base(id)
    {
        Name = name;
        MaxUsersCount = maxUsersCount;
        HostId = hostId;

        _users = new HashSet<UserId>();
        _graph = new NodeGraph(new NodeGraphPipeline());
    }

    public static Session Create(SessionCreationQuery query)
    {
        var session = new Session(query.Id, query.Name, query.MaxUsersCount, query.HostId);
        session.AddEvent(new SessionCreatedEvent(query.Id, query.HostId, query.Name));

        return session;
    }

    public UnitResult<NodeErrors> ApplyNodeGraphCommand<T>(T command) where T : class
    {
        return _graph.ApplyCommand(command);
    }

    public UnitResult<SessionErrors> JoinPlayer(UserId user)
    {
        if (_users.Contains(user) == true)
            return Result.Failure(SessionErrors.UserAlreadyExists, $"User with id {user.Id} already exists");

        bool result = _users.Add(user);

        if (result == false)
            return Result.Failure(SessionErrors.Common, $"Unable to join user with id {user.Id}");

        AddEvent(new UserJoinedEvent(Key, user));

        return Result.Success<SessionErrors>();
    }

    public UnitResult<SessionErrors> LeavePlayer(UserId user)
    {
        if (_users.Contains(user) == false)
            return Result.Failure(SessionErrors.UserNotExists, $"There is no user with id {user}");

        bool result = _users.Remove(user);//TODO: Handle host left

        if (result == false)
            return Result.Failure(SessionErrors.Common, $"Unable to remove user with id {user.Id}");

        AddEvent(new UserLeftEvent(Key, user));

        return Result.Success<SessionErrors>();
    }
}

public readonly record struct SessionCreationQuery(SessionId Id, string Name, int MaxUsersCount, UserId HostId);