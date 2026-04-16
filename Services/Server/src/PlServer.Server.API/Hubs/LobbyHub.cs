using Microsoft.AspNetCore.SignalR;

namespace PlServer.Server.API.Hubs;

public interface ILobbyHubClient
{
    Task LobbyChangedAsync();
}

public class LobbyHub : Hub<ILobbyHubClient>
{
}
