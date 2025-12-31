using PlServer.Domain;

namespace PlServer.Server.Infrastructure;

public interface IWorkerCoordinator
{
    Task<WorkResult> ExecuteWorkAsync(Work work);
}
