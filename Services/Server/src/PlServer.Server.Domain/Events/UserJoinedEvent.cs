
using PlServer.Domain;

namespace PlServer.Server.Domain.Events;

public record UserJoinedEvent(SessionId SessionId, UserId UserId) : IDomainEvent;
