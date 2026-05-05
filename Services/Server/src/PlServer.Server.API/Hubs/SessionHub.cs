using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;
using PlServer.Server.Infrastructure.NodeGraphs;
using PlServer.Server.Infrastructure.Sessions;
using PlServer.Server.Services;

namespace PlServer.Server.API.Hubs;

public interface ISessionClient
{
    Task SendMessageAsync(string message);

    Task ShutdownAsync(string? error = null);
}

[Authorize]
public class SessionHub : Hub<ISessionClient>
{
    private const string _sessionItemName = "sessionItem";
    private const string _userItemName = "userItem";

    private readonly ISessionConnectionTracker _tracker;
    private readonly ISessionService _service;
    private readonly ILogger<SessionHub> _logger;

    protected SessionId? SessionId => Context.Items[_sessionItemName] as SessionId?;

    protected UserId? UserId => Context.Items[_userItemName] as UserId?;

    public SessionHub(ISessionConnectionTracker tracker, ISessionService service, ILogger<SessionHub> logger)
    {
        _tracker = tracker;
        _service = service;
        _logger = logger;
    }

    [HubMethodName("Synchronize")]
    public Task<SynchronizeSnapshot> SyncronizeAsync(long version)
    {
        return Task.FromResult<SynchronizeSnapshot>(SynchronizeSnapshot.CreateFullSync(100));
    }
 
    public override async Task OnConnectedAsync()
    {
        if (SessionId == null || UserId == null)
        {
            await ShutdownAsync("Session is not valid");
            return;
        }

        var session = SessionId.Value;
        var user = UserId.Value;

        _logger.LogInformation(session.Id.ToString());

        var result = await _service.JoinAsync(session, user);

        if (result.IsSuccess == false && result.Error.Name != SessionErrors.UserAlreadyExists)
        {
            _logger.LogInformation(result.Error.Description);
            await ShutdownAsync(result.Error.Description);
            return;
        }

        _tracker.CreateConnection(Context.ConnectionId, SessionId.Value, UserId.Value);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connection = _tracker.RemoveConnection(Context.ConnectionId);

        if (connection != null)
            await _service.LeaveAsync(connection.Session, connection.User);

        await base.OnDisconnectedAsync(exception);
    }

    private async Task ShutdownAsync(string? error = null)
    {
        await Clients.Caller.ShutdownAsync(error);
        Context.Abort();
    }
}
