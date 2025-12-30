using PlServer.Worker.API.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<WorkerStartupService>();

var app = builder.Build();



app.Run();
