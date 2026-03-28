using Microsoft.AspNetCore.SignalR;

namespace PlServer.Server.API.Hubs;

public interface ISessionClient
{

}

public class SessionHub : Hub<ISessionClient>
{
    
}
