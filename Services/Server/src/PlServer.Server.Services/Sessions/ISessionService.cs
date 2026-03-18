namespace PlServer.Server.Services.Sessions;

public interface ISessionService
{
    ICollection<Session> Sessions { get; }

    Session? GetSession(Guid id);

    Task<Session?> CreateSessionAsync(string name);
}
