using Microsoft.AspNetCore.Mvc;
using PlServer.Server.API.Requests;
using PlServer.Server.API.Responces;
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;
using PlServer.Server.Services;

namespace PlServer.Server.API.Controllers;

[ApiController]
[Route("api/sessions/")]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessions;

    public SessionController(ISessionService sessions)
    {
        _sessions = sessions;
    }

    [HttpGet("all")]
    public IEnumerable<SessionResponse> GetSessionList()
    {
        var dtos = _sessions.GetSessionSummaryDtos();

        return dtos.Select(x => new SessionResponse(x.Name, x.Id, "user", x.MaxUsersCount, x.UsersCount));//TODO: user service, get user name
    }

    [HttpPost]
    public async Task<SessionId> CreateSession(SessionCreateRequest request)
    {
        var result = await _sessions.CreateSessionAsync(request.Name, UserId.New(), 5);

        return result.Value?.Id ?? SessionId.Empty;
    }
}
