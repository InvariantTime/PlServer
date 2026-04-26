
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Infrastructure.Sessions;

public record SessionConnection(SessionId Session, UserId User);