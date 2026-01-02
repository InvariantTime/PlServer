using Grpc.Net.ClientFactory;
using PlServer.Domain;
using PlServer.Protos;

namespace PlServer.Server.Infrastructure;

public class WorkerCoordinator : IWorkerCoordinator
{
    private readonly WorkerBridge.WorkerBridgeClient _client;

    public WorkerCoordinator(WorkerBridge.WorkerBridgeClient client)
    {
        _client = client;
    }

    public async Task<WorkResult> ExecuteWorkAsync(Work work)
    {
        var result = await _client.SendWorkAsync(new WorkRequest
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