using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Balancer;
using Grpc.Net.Client.Configuration;
using Microsoft.AspNetCore.Mvc;
using PlServer.Domain;
using PlServer.Protos;
using PlServer.Server.API.Hubs;
using PlServer.Server.API.Requests;
using PlServer.Server.Infrastructure;
using PlServer.Server.Infrastructure.Sessions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGrpc();
builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddSingleton<ISessionService, SessionService>();

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

builder.Services.AddSingleton<ResolverFactory>((scope) =>
{
    var factory = new StaticResolverFactory(addr =>
        {
            return [
                new BalancerAddress("worker1-host", 20000),
                new BalancerAddress("worker2-host", 20000)
            ];
        });

    return factory;
});

builder.Services.AddSingleton<LoadBalancerFactory>(_ =>
{
    return new RoundRobinBalancerFactory();
});

builder.Services.AddGrpcClient<WorkerBridge.WorkerBridgeClient>(op =>
{
    op.Address = new Uri("static:///worker-host");

}).ConfigureChannel((ch) =>
{
    ch.Credentials = ChannelCredentials.Insecure;
    ch.ServiceConfig = new ServiceConfig
    {
        LoadBalancingConfigs = { new RoundRobinConfig() }
    };
});

builder.Services.AddScoped<IWorkerCoordinator, WorkerCoordinator>();

var app = builder.Build();

app.UseCors("frontend");

app.MapPost("/work", async ([FromBody]WorkRestRequest request, IWorkerCoordinator coordinator) =>
{
    var result = await coordinator.ExecuteWorkAsync(new Work(request.Payload, request.Name));
    return result;
});

app.MapGet("ping", () => "pong");

app.MapHub<SessionHub>("ws/sessions");

app.MapControllers();

app.Run();