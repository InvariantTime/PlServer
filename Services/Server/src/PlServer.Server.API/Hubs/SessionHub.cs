using Microsoft.AspNetCore.SignalR;
using PlServer.Server.Infrastructure.Sessions;

namespace PlServer.Server.API.Hubs;

public interface ISessionClient
{
    Task SendLogHistoryAsync();

    Task LogDeletedAsync();

    Task LogSendedAsync();
}

public class SessionHub : Hub<ISessionClient>
{
    public const string QueryName = "sessionId";

    private readonly ISessionService _sessions;

    public SessionHub(ISessionService sessions)
    {
        _sessions = sessions;
    }

    public async Task ExecuteProcedureAsync()
    {

    }

    public override async Task OnConnectedAsync()
    {
        var idRaw = Context.GetHttpContext()!.Request.Query[QueryName];

        if (Guid.TryParse(idRaw, out var id) == false)
            return;

        var session = _sessions.GetSession(id);

        if (session == null)
            return;

        await Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());

        await OnConnectedAsync();
    }
}
