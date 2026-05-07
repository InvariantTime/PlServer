
using PlServer.Domain.Nodes;
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Infrastructure.Sessions;

public record SessionConnection(SessionId Session, NodeGraphId NodeGraph, UserId User, string Connection);