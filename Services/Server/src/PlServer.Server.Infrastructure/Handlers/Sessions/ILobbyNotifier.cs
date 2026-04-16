
namespace PlServer.Server.Infrastructure.Handlers.Sessions;

public interface ILobbyNotifier
{
    Task NotifyLobbyChangedAsync();
}
