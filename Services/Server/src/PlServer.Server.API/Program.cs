using Microsoft.AspNetCore.SignalR;
using PlServer.Application;
using PlServer.Server.API;
using PlServer.Server.API.Binders;
using PlServer.Server.API.Hubs;
using PlServer.Server.Domain.Events;
using PlServer.Server.Infrastructure;
using PlServer.Server.Infrastructure.Handlers.Sessions;

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

builder.Services.AddSingleton<ILobbyNotifier, SessionLobbyNotifier>();

builder.Services.AddEventHandling()
    .RegisterRepositories()
    .RegisterDatabases(configuration)
    .RegisterServices()
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