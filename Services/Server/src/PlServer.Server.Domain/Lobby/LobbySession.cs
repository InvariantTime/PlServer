
using PlServer.Domain;

namespace PlServer.Server.Domain.Lobby;

public class LobbySession : AggregateRoot<Guid>
{
    public string Name { get; }

    public int PlayersCount { get; private set; }

    public int MaxPlayersCount { get; }

    private LobbySession(string name, Guid id) : base(id)
    {
        Name = name;
    }

    public static LobbySession Create(string name, Guid id)
    {
        var session = new LobbySession(name, id);
        session.AddEvent(new LobbySessionCreatedEvent(id, name));

        return session;
    }
}
