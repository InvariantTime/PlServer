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
        await OnConnectedAsync();
    }
}
