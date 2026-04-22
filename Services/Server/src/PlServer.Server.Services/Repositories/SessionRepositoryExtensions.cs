
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Services.Repositories;

public static class SessionRepositoryExtensions
{
    public static bool CanJoinTo(this ISessionRepository repository, UserId user, SessionId session)
    {
        var hosted = repository.GetSessionByHost(user);
        var connected = repository.GetSessionByUser(user);

        if (hosted == session)
            return connected == null;

        return connected == null && hosted == null;
    }

    public static bool IsHostUser(this ISessionRepository repository, UserId user)
    {
        return repository.GetSessionByHost(user) != null;
    }

    public static bool CanCreateSession(this ISessionRepository repository, UserId user)
    {
        var hosted = repository.GetSessionByHost(user);
        var connected = repository.GetSessionByUser(user);

        return hosted == null && connected == null;
    }
}
