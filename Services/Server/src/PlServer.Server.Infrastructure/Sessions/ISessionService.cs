namespace PlServer.Server.Infrastructure.Sessions;

public interface ISessionService
{
    ICollection<Session> Sessions { get; }

    Session? GetSession(Guid id);

    Session CreateSession(string name);
}
