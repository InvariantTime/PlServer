namespace PlServer.Server.Infrastructure.Handlers.Lobby;

public interface ILobbyNotifier
{
    Task NotifyLobbyChangedAsync();
}
