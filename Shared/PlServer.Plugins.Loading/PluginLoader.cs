using PlServer.Domain.Executables;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Loader;

namespace PlServer.Plugins.Loading;

public class PluginLoader : IPluginLoader
{
    private readonly ConcurrentDictionary<string, PluginDescription?> _decriptions;
    private readonly PluginLoadContext _context;

    public PluginLoader(string path)
    {
        _context = new PluginLoadContext(path);
        _decriptions = new ConcurrentDictionary<string, PluginDescription?>();
    }

    public PluginDescription? LoadPlugin(string name)
    {
        return _decriptions.GetOrAdd(name, CreateDescription);
    }

    private PluginDescription? CreateDescription(string name)
    {
        var assembly = _context.LoadFromAssemblyName(new AssemblyName(name));

        if (assembly == null)
            return null;

        var types = assembly.GetTypes();

        var pluginClass = types.SingleOrDefault(x => typeof(IPlugin).IsAssignableFrom(x) == true);

        if (pluginClass == null)
            return null;

        var plugin = (IPlugin)Activator.CreateInstance(pluginClass)!;
        var executors = types.Where(x => typeof(IExecutable).IsAssignableFrom(x))
            .Select(Activator.CreateInstance)
            .Cast<IExecutable>();

        return new PluginDescription(plugin, executors);
    }
}
