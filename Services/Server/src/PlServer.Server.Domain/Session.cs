
using PlServer.Domain;
using PlServer.Domain.Nodes;
using PlServer.Domain.Results;
using PlServer.Server.Domain.Events;

namespace PlServer.Server.Domain;

public class Session : AggregateRoot<SessionId>
{
    private readonly HashSet<UserId> _users;

    public UserId HostId { get; }

    public string Name { get; }

    public IReadOnlyCollection<UserId> Users => _users.AsReadOnly();

    public int MaxUsersCount { get; }

    public NodeGraphId GraphId { get; }

    private Session(SessionId id, string name, int maxUsersCount, UserId hostId, NodeGraphId graphId) : base(id)
    {
        Name = name;
        MaxUsersCount = maxUsersCount;
        HostId = hostId;
        GraphId = graphId;

        _users = new HashSet<UserId>();//TODO: move all user logic to new class
    }

    public static Session Create(SessionCreationQuery query)
    {
        var session = new Session(query.Id, query.Name, query.MaxUsersCount, query.HostId, query.GraphId);
        session.AddEvent(new SessionCreatedEvent(query.Id, query.GraphId, query.HostId, query.Name));

        return session;
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

public readonly record struct SessionCreationQuery(SessionId Id, string Name, int MaxUsersCount, UserId HostId, NodeGraphId GraphId);