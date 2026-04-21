using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PlServer.Server.API.Options;
using PlServer.Server.API.Requests;
using PlServer.Server.API.Responces;
using PlServer.Server.Domain.Users;
using PlServer.Server.Infrastructure.Auth;
using PlServer.Server.Services;
using PlServer.Server.Services.DTOs;

namespace PlServer.Server.API.Controllers;

[ApiController]
[Route("api/users/")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IAuthTokenService _tokenService;
    private readonly CookieNamesOptions _options;

    public UserController(IUserService service, IAuthTokenService tokenService, IOptions<CookieNamesOptions> options)
    {
        _service = service;
        _tokenService = tokenService;
        _options = options.Value;
    }

    [HttpPost("register")]
    public async Task<IResult> RegisterAsync([FromBody]RegisterRequest request)
    {
        var result = await _service.RegisterAsync(request.Name, request.Password);

        if (result.IsSuccess == false)
            return Results.BadRequest(result.AsErrorResponce());

        var token = _tokenService.GenerateToken(result.Value!);
        HttpContext.Response.Cookies.Append(_options.AuthCookieName, token);

        return Results.Ok();
    }

    [HttpPost("login")]
    public async Task<IResult> LogInAsync(LoginRequest request)
    {
        var result = await _service.LoginAsync(request.Name, request.Password);

        if (result.IsSuccess == false)
            return Results.BadRequest(result.AsErrorResponce());

        var token = _tokenService.GenerateToken(result.Value!);
        HttpContext.Response.Cookies.Append(_options.AuthCookieName, token);

        return Results.Ok();
    }

    [Authorize]
    [HttpGet("verify")]
    public IResult Verify(UserSummaryDTO user)//TODO: send name to client
    {
        return Results.Ok();
    }
}
