
using PlServer.Domain;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Domain.Events;

public record UserJoinedEvent(SessionId SessionId, UserId UserId) : ISessionEvent;
