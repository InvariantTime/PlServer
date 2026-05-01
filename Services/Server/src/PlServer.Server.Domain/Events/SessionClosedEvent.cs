
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Domain.Events;

public record SessionClosedEvent(SessionId SessionId, string Name, IReadOnlyUserCollection Users) : ISessionEvent;
