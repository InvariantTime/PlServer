namespace PlServer.Server.Infrastructure.Sessions;

public interface ISessionService
{
    Session? GetSession(Guid id);

    void CreateSession();
}
