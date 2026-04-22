using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Services.Repositories;

public interface ISessionRepository
{
    Session? GetSessionById(SessionId session);

    SessionId? GetSessionByUser(UserId user);

    SessionId? GetSessionByHost(UserId host);

    bool AddSession(Session session);

    Session? RemoveSession(SessionId session);

    void Update(Session session);

    ICollection<Session> GetAll();
}