using Microsoft.AspNetCore.SignalR;
using PlServer.Server.API.Options;
using PlServer.Server.Domain;
using PlServer.Server.Infrastructure.Auth;

namespace PlServer.Server.API.Hubs;

public class SessionHubFilter : IHubFilter
{
    private const string _sessionIdQuery = "sessionId";

    private const string _userItemName = "userItem";
    private const string _sessionItemName = "sessionItem";

    private readonly IAuthTokenService _auth;
    private readonly CookieNamesOptions _cookies;

    public SessionHubFilter(IAuthTokenService auth, CookieNamesOptions cookies)
    {
        _auth = auth;
        _cookies = cookies;
    }

    public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
    {
        var http = context.Context.GetHttpContext()!;

        var sessionQuery = http.Request.Query[_sessionIdQuery];
        var cookie = http.Request.Cookies[_cookies.AuthCookieName];

        var user = _auth.ValidateToken(cookie);

        if (user == null || SessionId.TryParse(sessionQuery, out var session) == false)
            return next(context);

        context.Context.Items[_userItemName] = user;
        context.Context.Items[_sessionItemName] = session;

        return next(context);
    }
}