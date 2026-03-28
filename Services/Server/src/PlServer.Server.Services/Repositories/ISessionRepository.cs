using PlServer.Server.Domain;

namespace PlServer.Server.Services.Repositories;

public interface ISessionRepository
{
    Session? GetSessionById(SessionId session);

    bool AddSession(Session session);

    Session? RemoveSession(SessionId session);

    ICollection<Session> GetAll();
}