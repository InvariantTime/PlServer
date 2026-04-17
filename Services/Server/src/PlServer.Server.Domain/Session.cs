
using PlServer.Domain;
using PlServer.Domain.Nodes;
using PlServer.Domain.Results;
using PlServer.Server.Domain.Events;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Domain;

public class Session : AggregateRoot<SessionId, ISessionEvent>
{
    private readonly UserCollection _users;

    public string Name { get; }

    public NodeGraphId GraphId { get; }

    public IReadOnlyUserCollection Users => field 
        ?? new ReadOnlyUserCollection(_users);

    private Session(SessionId id, string name, int maxUsersCount, UserId hostId, NodeGraphId graphId) : base(id)
    {
        Name = name;
        GraphId = graphId;

        _users = new UserCollection(hostId, maxUsersCount);
    }

    public static Session Create(SessionCreationQuery query)
    {
        var session = new Session(query.Id, query.Name, query.MaxUsersCount, query.HostId, query.GraphId);
        session.AddEvent(new SessionCreatedEvent(query.Id, query.GraphId, query.HostId, query.Name));

        return session;
    }

    public UnitResult<SessionErrors> JoinPlayer(User user)
    {
        var result = _users.TryAdd(user);

        if (result.IsSuccess == true)
            AddEvent(new UserJoinedEvent(Key, user.Key));

        return result;
    }

    public UnitResult<SessionErrors> LeavePlayer(UserId user)
    {
        var result = _users.TryRemove(user, out var isHost);

        if (result.IsSuccess == false)
            return result;

        if (isHost == true)
            AddEvent(new HostLeftEvent(Key, user));

        AddEvent(new UserLeftEvent(Key, user));

        return Result.Success<SessionErrors>();
    }
}

public readonly record struct SessionCreationQuery(SessionId Id, string Name, int MaxUsersCount, UserId HostId, NodeGraphId GraphId);