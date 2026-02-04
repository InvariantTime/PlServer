using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PlServer.Server.API.Hubs;
using PlServer.Server.API.Responces;
using PlServer.Server.Infrastructure.Sessions;

namespace PlServer.Server.API.Controllers;

[ApiController]
[Route("api/sessions/")]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessions;
    private readonly IHubContext<SessionHub, ISessionClient> _hub;

    public SessionController(ISessionService sessions, IHubContext<SessionHub, ISessionClient> hub)
    {
        _sessions = sessions;
        _hub = hub;
    }

    [HttpGet("all")]
    public IEnumerable<SessionResponce> GetSessionList()
    {
        return _sessions.Sessions.Select(x => new SessionResponce(x.Name, x.Id));
    }

    [HttpPost]
    public Task<Guid> CreateSession(string name)
    {
        var session = _sessions.CreateSession(name);

        var sessions = _sessions.Sessions;
        _hub.Clients.Group("Lobby").OnSessionListChangedAsync(sessions.Select(x => new SessionResponce(x.Name, x.Id)));//TODO: migrate hubs to infrastructure layer and make notification insert the service

        return Task.FromResult(session.Id);
    }
}
