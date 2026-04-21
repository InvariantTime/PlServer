using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PlServer.Server.API.Options;
using PlServer.Server.Infrastructure.Auth;
using System.Text;

namespace PlServer.Server.API;

public static class AuthExtensions
{
    public const string CookieNamesSection = "CookieNamesOptions";
    public const string AuthOptionsSection = "AuthOptions";

    public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var cookies = configuration.GetSection(CookieNamesSection).Get<CookieNamesOptions>()!;
        var auth = configuration.GetSection(AuthOptionsSection).Get<AuthOptions>()!;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(auth.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = (context) =>
                    {
                        context.Token = context.HttpContext.Request.Cookies[cookies.AuthCookieName];
                        return Task.CompletedTask;
                    }
                };
            });

        services.Configure<AuthOptions>(configuration.GetSection(AuthOptionsSection));
        services.Configure<CookieNamesOptions>(configuration.GetSection(CookieNamesSection));
    }
}
