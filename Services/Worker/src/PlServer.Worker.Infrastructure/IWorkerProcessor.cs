
using PlServer.gRPC.Protos;

namespace PlServer.Worker.Infrastructure;

public interface IWorkerProcessor
{
    ProcessStatus GetStatus();

    Task AddWorkToQueueAsync(ExecutionChainData chain);
}

public record ProcessStatus(int FreeSlots, int MaxSlots);