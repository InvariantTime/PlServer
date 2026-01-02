using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using PlServer.Domain;
using PlServer.Protos;
using PlServer.Server.API.Requests;
using PlServer.Server.Infrastructure;
using PlServer.Server.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGrpc();

builder.Services.AddSingleton<IChannelProvider>((_) =>
{
    string[] hosts = ["http://worker1-host:20000", "http://worker2-host:20000"];
    return new ChannelProvider(hosts);
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
