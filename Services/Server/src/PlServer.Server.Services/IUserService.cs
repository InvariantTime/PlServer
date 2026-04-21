
using PlServer.Domain.Results;
using PlServer.Server.Domain.Users;
using PlServer.Server.Services.DTOs;

namespace PlServer.Server.Services;

public interface IUserService
{
    Task<Result<UserId>> RegisterAsync(string name, string passwor);

    Task<Result<UserId>> LoginAsync(string name, string password);

    UserSummaryDTO? GetUserById(UserId id);
}
