using PlServer.Server.Domain.Lobby;

namespace PlServer.Server.Services.Lobby;

public interface ILobbySessionService
{
    IReadOnlyCollection<LobbySession> Sessions { get; }

    Task<Guid> CreateSessionAsync(string name);
}
