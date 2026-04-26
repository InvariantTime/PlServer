using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PlServer.Server.API.Responces;
using PlServer.Server.Domain;
using PlServer.Server.Infrastructure.Sessions;
using PlServer.Server.Services;
using PlServer.Server.Services.DTOs;

namespace PlServer.Server.API.Hubs;

public interface ISessionClient
{

    Task SendMessageAsync(ErrorResponce error);

    Task ShutdownAsync(string? error = null);
}

[Authorize]
public class SessionHub : Hub<ISessionClient>
{
    private const string _sessionIdQuery = "sessionId";
    private const string _userItemName = "userItem";

    private readonly ISessionConnectionTracker _tracker;
    private readonly ISessionService _service;

    public SessionHub(ISessionConnectionTracker tracker, ISessionService service)
    {
        _tracker = tracker;
        _service = service;
    }

    public override async Task OnConnectedAsync()
    {
        var sessionIdRaw = Context.GetHttpContext()!.Request.Query[_sessionIdQuery];
        var userRaw = Context.Items[_userItemName];

        await OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connection = _tracker.RemoveConnection(Context.ConnectionId);

        if (connection != null)
            await _service.LeaveAsync(connection.Session, connection.User);

        await base.OnDisconnectedAsync(exception);
    }
}
