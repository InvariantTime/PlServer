using Microsoft.AspNetCore.Mvc;
using PlServer.Server.API.Requests;
using PlServer.Server.API.Responces;
using PlServer.Server.Services;

namespace PlServer.Server.API.Controllers;

[ApiController]
[Route("api/users/")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IResult> RegisterAsync([FromBody]RegisterRequest request)
    {
        var result = await _service.RegisterAsync(request.Name, request.Password);

        if (result.IsSuccess == false)
            return Results.BadRequest(result.AsErrorResponce());

        return Results.Ok();
    }

    [HttpPost("login")]
    public async Task<IResult> LogInAsync(LoginRequest request)
    {
        var result = await _service.LoginAsync(request.Name, request.Password);

        if (result.IsSuccess == false)
            return Results.BadRequest(result.AsErrorResponce());

        return Results.Ok();
    }
}
