using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PlServer.Application;
using PlServer.Server.API;
using PlServer.Server.API.Binders;
using PlServer.Server.API.Hubs;
using PlServer.Server.Infrastructure;
using PlServer.Server.Infrastructure.Auth;
using PlServer.Server.Infrastructure.Handlers.Sessions;
using PlServer.Server.Infrastructure.Hashers;
using PlServer.Server.Infrastructure.Persistence;
using PlServer.Server.Infrastructure.Repositories;
using PlServer.Server.Infrastructure.Sessions;
using PlServer.Server.Services;
using PlServer.Server.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR()
    .AddHubOptions<SessionHub>(op =>
    {
        op.AddFilter<SessionHubFilter>();
    });


builder.Services.AddControllers(op =>
{
    op.ModelBinderProviders.Insert(0, new CustomBindingProvider());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(op =>
{
    op.UseNpgsql(configuration.GetConnectionString("postgres"));
});

builder.Services.AddSingleton<ISessionRepository, InMemorySessionRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

builder.Services.AddSingleton<ISessionService, SessionService>();
builder.Services.AddSingleton<ILobbyNotifier, SessionLobbyNotifier>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IAuthTokenService, JwtAuthTokenService>();

builder.Services.AddSingleton<ISessionConnectionTracker, SessionConnectionTracker>();

builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.RegisterHandlers();


var app = builder.Build();

app.UseCors("frontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("ping", () => "pong");

app.MapControllers();

app.MapHub<LobbyHub>("ws/lobby");
app.MapHub<SessionHub>("ws/sessions");

app.Run();