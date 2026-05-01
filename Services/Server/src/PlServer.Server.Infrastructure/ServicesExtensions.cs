using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlServer.Server.Infrastructure.Persistence;
using PlServer.Server.Infrastructure.Repositories;
using PlServer.Server.Infrastructure.Sessions;
using PlServer.Server.Services;
using PlServer.Server.Services.Repositories;

namespace PlServer.Server.Infrastructure;

public static class ServicesExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IUserService, UserService>();

        services.AddSingleton<ISessionConnectionTracker, SessionConnectionTracker>();

        return services;
    }

    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, EfUserRepository>();
        services.AddSingleton<ISessionRepository, InMemorySessionRepository>();

        return services;
    }

    public static IServiceCollection RegisterDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(op =>
        {
            op.UseNpgsql(configuration.GetConnectionString("postgres"));
        });

        return services;
    }
}
