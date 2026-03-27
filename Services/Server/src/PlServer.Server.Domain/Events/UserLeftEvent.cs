
using PlServer.Domain;

namespace PlServer.Server.Domain.Events;

public record UserLeftEvent(SessionId SessionId, UserId UserId) : IDomainEvent;