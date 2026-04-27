
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Infrastructure.Auth;

public interface IAuthTokenService
{
    string GenerateToken(UserId user);

    UserId? ValidateToken(string? token);
}
