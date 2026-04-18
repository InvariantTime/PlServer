
using PlServer.Domain.Results;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Services;

public interface IUserService
{
    Task<Result<UserId>> RegisterAsync(string name, string passwordHash);

    Task<Result<UserId>> LoginAsync(string name, string passwordHash);
}
