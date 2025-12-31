using Grpc.Core;
using PlServer.Protos;
using System.Diagnostics;

namespace PlServer.Worker.API.Services;

public class WorkConsumerService : WorkerBridge.WorkerBridgeBase
{
    public override async Task<WorkResponse> SendWork(WorkRequest request, ServerCallContext context)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();

        await Task.Delay(request.Payload, context.CancellationToken);

        watch.Stop();

        return new WorkResponse
        {
            Result = $"Work {request.Name} completed at {} seconds"
        };
    }
}
