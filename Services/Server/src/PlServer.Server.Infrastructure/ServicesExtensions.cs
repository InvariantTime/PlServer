using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

    public static IServiceCollection RegisterRepositories(this IServiceCollection services, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment() == true)
        {
            services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        }
        else
        {
            services.AddScoped<IUserRepository, EfUserRepository>();
        }

        services.AddSingleton<ISessionRepository, InMemorySessionRepository>();

        return services;
    }

    public static IServiceCollection RegisterDatabases(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment() == true)
            return services;

        services.AddDbContext<ApplicationDbContext>(op =>
        {
            op.UseNpgsql(configuration.GetConnectionString("postgres"));
        });

        return services;
    }
}
