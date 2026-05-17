using Grpc.Core;
using PlServer.gRPC.Protos;

namespace PlServer.Worker.Infrastructure;

public class MessageConsumer : WorkerService.WorkerServiceBase
{
    private readonly IPluginService _plugins;
    private readonly IWorkerProcessor _processor;

    public MessageConsumer(IPluginService plugins, IWorkerProcessor processor)
    {
        _plugins = plugins;
        _processor = processor;
    }

    public override async Task<ExecutionResponse> Execute(ExecutionRequest request, ServerCallContext context)
    {
        var awaiter = _processor.AddWorkToQueue(request.Chain);

        var result = await awaiter;

        return result.ToResponse();
    }

    public override Task<StatusResponse> GetStatus(StatusRequest request, ServerCallContext context)
    {
        var plugins = _plugins.GetLoadedPlugins();
        var status = _processor.GetStatus();

        StatusResponse response = new StatusResponse();
        response.LoadedPlugins.AddRange(plugins);
        response.FreeSlots = status.FreeSlots;
        response.MaxSlots = status.MaxSlots;

        return Task.FromResult(response);
    }
}
