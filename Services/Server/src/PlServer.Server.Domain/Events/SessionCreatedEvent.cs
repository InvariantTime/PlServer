
using PlServer.Domain;

namespace PlServer.Server.Domain.Events;

public record SessionCreatedEvent(SessionId SessionId, UserId HostId, string Name) : IDomainEvent;