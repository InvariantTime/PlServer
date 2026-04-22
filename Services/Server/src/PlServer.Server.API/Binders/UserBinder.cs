using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using PlServer.Server.API.Options;
using PlServer.Server.Infrastructure.Auth;
using PlServer.Server.Services;

namespace PlServer.Server.API.Bindings;

public class UserBinder : IModelBinder
{
    private readonly IUserService _users;
    private readonly IAuthTokenService _tokenService;
    private readonly CookieNamesOptions _options;

    public UserBinder(IUserService users, IAuthTokenService tokenService, IOptions<CookieNamesOptions> options)
    {
        _users = users;
        _tokenService = tokenService;
        _options = options.Value;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var http = bindingContext.HttpContext;
        var token = http.Request.Cookies[_options.AuthCookieName];

        if (token == null)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var id = _tokenService.ValidateToken(token);

        if (id == null)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var user = _users.GetUserById(id.Value);

        if (user == null)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(user);
        return Task.CompletedTask;
    }
}
