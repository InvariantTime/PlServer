
using PlServer.gRPC.Protos;

namespace PlServer.Worker.Infrastructure;

public interface IWorkerProcessor
{
    ProcessStatus GetStatus();

    Task<int> AddWorkToQueue(ExecutionChainData chain);
}

public record ProcessStatus(int FreeSlots, int MaxSlots);

public static class IntExtension
{
    public static ExecutionResponse ToResponse(this int i)
    {
        return new ExecutionResponse();
    }
}