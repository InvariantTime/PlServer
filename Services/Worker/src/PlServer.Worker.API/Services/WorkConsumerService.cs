using Grpc.Core;
using PlServer.Protos;
using System.Diagnostics;

namespace PlServer.Worker.API.Services;

public class WorkConsumerService : WorkerBridge.WorkerBridgeBase
{
    private readonly string _name; 

    public WorkConsumerService(IConfiguration configuration)
    {
        _name = configuration.GetSection("WorkerOptions").GetValue<string>("Name") ?? string.Empty;
    }

    public override async Task<WorkResponse> SendWork(WorkRequest request, ServerCallContext context)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();

        await Task.Delay(request.Payload, context.CancellationToken);

        watch.Stop();

        return new WorkResponse
        {
            Result = $"Work {request.Name} completed in {watch.Elapsed.TotalSeconds} seconds, by worker {_name}"
        };
    }
}
