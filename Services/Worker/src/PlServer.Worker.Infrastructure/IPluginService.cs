
namespace PlServer.Worker.Infrastructure;

public interface IPluginService
{
    IEnumerable<string> GetLoadedPlugins();
}
