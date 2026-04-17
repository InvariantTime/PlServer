
using PlServer.Domain;
using PlServer.Domain.Nodes;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Domain.Events;

public record SessionCreatedEvent(SessionId SessionId, NodeGraphId GraphId, UserId HostId, string Name) : ISessionEvent;