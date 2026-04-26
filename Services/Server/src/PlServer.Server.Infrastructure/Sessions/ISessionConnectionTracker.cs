
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Infrastructure.Sessions;

public interface ISessionConnectionTracker
{
    SessionConnection CreateConnection(string id, SessionId session, UserId user);

    SessionConnection? RemoveConnection(string id);

    SessionConnection? GetConnection(string id);

    ICollection<SessionConnection> GetAll(SessionId session);
}