using PlServer.Domain;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Domain.Events;

public record UserRegisteredEvent(UserId Id, string Name) : IDomainEvent;