
using PlServer.Domain;

namespace PlServer.Server.Domain.Lobby;

public record LobbySessionCreatedEvent(Guid SessionId, string Name) : IDomainEvent;
