using PlServer.Domain;

namespace PlServer.Server.Domain.Events;

public record HostLeftEvent(SessionId SessionId, UserId UserId) : IDomainEvent;