using Microsoft.AspNetCore.Mvc;
using PlServer.Domain;
using PlServer.Protos;
using PlServer.Server.API.Requests;
using PlServer.Server.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcClient<WorkerBridge.WorkerBridgeClient>((options) =>
{
    options.Address = new Uri("http://worker-host:20000");
});

builder.Services.AddScoped<IWorkerCoordinator, WorkerCoordinator>();

var app = builder.Build();

app.MapPost("/work", async ([FromBody]WorkRestRequest request, IWorkerCoordinator coordinator) =>
{
    var result = await coordinator.ExecuteWorkAsync(new Work(request.Payload, request.Name));
    return result;
});

app.MapGet("ping", () => "pong");


app.Run();
