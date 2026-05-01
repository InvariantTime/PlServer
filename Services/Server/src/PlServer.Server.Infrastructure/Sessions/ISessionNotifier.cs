using PlServer.Server.Domain;

namespace PlServer.Server.Infrastructure.Sessions;

public interface ISessionNotifier
{
    Task HandleShutdownAsync(SessionId session);
}
