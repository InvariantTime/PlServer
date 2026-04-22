using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlServer.Server.Domain.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlServer.Server.Infrastructure.Auth;

public class JwtAuthTokenService : IAuthTokenService
{
    private readonly AuthOptions _options;
    private readonly JwtSecurityTokenHandler _handler;

    public JwtAuthTokenService(IOptions<AuthOptions> options)
    {
        _options = options.Value;
        _handler = new JwtSecurityTokenHandler();
    }

    public string GenerateToken(UserId user)
    {
        Claim[] claims = [new Claim(_options.UserIdClaim, user.Id.ToString())];

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddHours(_options.ExpitesHours);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials,
            expires: expires);

        return _handler.WriteToken(token);
    }

    public UserId? ValidateToken(string key)
    {
        var token = _handler.ReadJwtToken(key);

        var idClaim = token.Claims.FirstOrDefault(x => x.Type == _options.UserIdClaim);

        if (idClaim == null)
            return null;

        return UserId.FromString(idClaim.Value);
    }
}
