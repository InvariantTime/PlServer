using PlServer.Application;
using PlServer.Server.API.Hubs;
using PlServer.Server.Infrastructure;
using PlServer.Server.Infrastructure.Handlers.Sessions;
using PlServer.Server.Infrastructure.Hashers;
using PlServer.Server.Infrastructure.Repositories;
using PlServer.Server.Services;
using PlServer.Server.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddControllers();

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

builder.Services.AddSingleton<ISessionRepository, InMemorySessionRepository>();
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();

builder.Services.AddSingleton<ISessionService, SessionService>();
builder.Services.AddSingleton<ILobbyNotifier, SessionLobbyNotifier>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

builder.Services.RegisterHandlers();


var app = builder.Build();

app.UseCors("frontend");

app.MapGet("ping", () => "pong");

app.MapControllers();

app.MapHub<LobbyHub>("ws/lobby");

app.Run();