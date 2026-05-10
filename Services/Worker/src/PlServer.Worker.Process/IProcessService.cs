
namespace PlServer.Worker.Process;

public interface IProcessService
{
    Task StartProcessAsync();

    Task StopProcessAsync(ProcessId id);

    IEnumerable<PluginProcess> GetAllProcesses();
}
