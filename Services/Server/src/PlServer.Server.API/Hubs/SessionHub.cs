using Microsoft.AspNetCore.SignalR;
using PlServer.Server.API.Responces;
using PlServer.Server.Infrastructure.Sessions;

namespace PlServer.Server.API.Hubs;

public interface ISessionLobbyClient
{
    Task OnSessionListChangedAsync(IEnumerable<SessionResponce> sessions);
}

public interface ISessionClient : ISessionLobbyClient
{
}

public class SessionHub : Hub<ISessionClient>
{
    public const string QueryName = "sessionId";

    private readonly ISessionService _sessions;

    public SessionHub(ISessionService sessions)
    {
        _sessions = sessions;
    }

    public override Task OnConnectedAsync()
    {
        var id = Context.GetHttpContext()!.Request.Query[QueryName];

        if (string.IsNullOrEmpty(id) == false)
        {

        }

        return base.OnConnectedAsync();
    }
}
