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
    private readonly INodeGraphProvider _nodeGraphs;

    protected SessionId? SessionId => Context.Items[_sessionItemName] as SessionId?;

    protected UserId? UserId => Context.Items[_userItemName] as UserId?;

    public SessionHub(ISessionConnectionTracker tracker, ISessionService service, INodeGraphProvider nodeGraphs)
    {
        _tracker = tracker;
        _service = service;
        _nodeGraphs = nodeGraphs;
    }

    [HubMethodName("Synchronize")]
    public Task<SynchronizeSnapshot> SyncronizeAsync(long version)
    {
        var connection = _tracker.GetConnection(Context.ConnectionId);

        if (connection == null)
            return Task.FromResult<SynchronizeSnapshot>(null!);//TODO: handle

        return _nodeGraphs.SyncAsync(connection.NodeGraph, version);
    }

    [HubMethodName("HandleCommand")]
    public Task HandleCommandAsync(NodeGraphCommand command)
    {
        var connection = _tracker.GetConnection(Context.ConnectionId);

        if (connection == null)
            return Task.CompletedTask;

        return _nodeGraphs.ApplyCommandAsync(command, connection.NodeGraph, connection.User);
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

        var result = await _service.JoinAsync(session, user);

        if (result.IsSuccess == false && result.Error.Name != SessionErrors.UserAlreadyExists)
        {
            await ShutdownAsync(result.Error.Description);
            return;
        }

        var nodeGraph = _service.GetSessionSummaryDtos().First(x => x.Id == SessionId).NodeGraph;

        _tracker.CreateConnection(Context.ConnectionId, SessionId.Value, nodeGraph, UserId.Value);
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
