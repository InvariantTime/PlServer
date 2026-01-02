using PlServer.Domain;
using PlServer.Protos;
using PlServer.Server.Infrastructure.Services;

namespace PlServer.Server.Infrastructure;

public class WorkerCoordinator : IWorkerCoordinator
{
    private readonly IChannelProvider _channels;

    public WorkerCoordinator(IChannelProvider channels)
    {
        _channels = channels;
    }

    public async Task<WorkResult> ExecuteWorkAsync(Work work)
    {
        var channel = _channels.GetGrpcChannel();
        var client = new WorkerBridge.WorkerBridgeClient(channel);

        var result = await client.SendWorkAsync(new WorkRequest
        {
            Payload = work.Payload,
            Name = work.Name,
        });

        return new WorkResult
        {
            Value = result.Result,
        };
    }
}