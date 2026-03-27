using PlServer.Server.API.Hubs;
using PlServer.Server.Services;

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

//builder.Services.AddSingleton<ISessionService, SessionService>();

var app = builder.Build();

app.UseCors("frontend");

app.MapGet("ping", () => "pong");

app.MapControllers();

app.MapHub<SessionHub>("ws/sessions"); 

app.Run();