using Microsoft.AspNetCore.SignalR;
using PlServer.Server.API;
using PlServer.Server.API.Binders;
using PlServer.Server.API.Converters;
using PlServer.Server.API.Hubs;
using PlServer.Server.Infrastructure;
using PlServer.Server.Infrastructure.Handlers.Lobby;
using PlServer.Server.Infrastructure.Sessions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR()
    .AddJsonProtocol(op =>
    {
        op.PayloadSerializerOptions.Converters.Add(new NodeIdConverter());
        op.PayloadSerializerOptions.Converters.Add(new PinIdConverter());
    })
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
var environment = builder.Environment;

builder.Services.AddSingleton<ILobbyNotifier, SessionLobbyNotifier>();
builder.Services.AddSingleton<ISessionNotifier, SessionNotifier>();

builder.Services.AddEventHandling()
    .RegisterRepositories(environment)
    .RegisterDatabases(configuration, environment)
    .RegisterServices()
    .RegisterHostedServices()
    .RegisterAuthentication(configuration);


var app = builder.Build();

app.UseCors("frontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("ping", () => "pong");

app.MapControllers();

app.MapHub<LobbyHub>("ws/lobby");
app.MapHub<SessionHub>("ws/sessions");

app.Run();