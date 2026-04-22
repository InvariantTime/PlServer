using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlServer.Server.API.Requests;
using PlServer.Server.API.Responces;
using PlServer.Server.Domain;
using PlServer.Server.Services;
using PlServer.Server.Services.DTOs;

namespace PlServer.Server.API.Controllers;

[ApiController]
[Route("api/sessions/")]
[Authorize]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessions;
    private readonly IUserService _users;

    public SessionController(ISessionService sessions, IUserService users)
    {
        _sessions = sessions;
        _users = users;
    }

    [HttpGet("all")]
    public IEnumerable<SessionResponse> GetSessionList()
    {
        var dtos = _sessions.GetSessionSummaryDtos();

        return GetResponseAll(dtos);
    }

    [HttpPost]
    public async Task<SessionId> CreateSession([FromBody]SessionCreateRequest request, [FromServices]UserSummaryDTO user)
    {
        var result = await _sessions.CreateSessionAsync(request.Name, user.Id, 5);

        return result.Value?.Id ?? SessionId.Empty;
    }

    private IEnumerable<SessionResponse> GetResponseAll(IEnumerable<SessionSummaryDTO> sessions)
    {
        return sessions.Select(x => new SessionResponse
        {
            Id = x.Id,
            Name = x.Name,
            UserCount = x.Users.UserCount,
            MaxUserCount = x.Users.MaxUserCount,
            HostName = _users.GetUserById(x.Users.HostId)?.Name ?? string.Empty 
        });
    }
}
