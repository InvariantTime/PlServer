
using PlServer.Worker.API.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<WorkConsumerService>();

app.Run();
