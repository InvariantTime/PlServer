namespace PlServer.Server.Services.Sessions;

public interface ISessionNotificationService
{
    Task NotifyLobbyChangedAsync(IEnumerable<Session> sessions, CancellationToken cancellation);
}