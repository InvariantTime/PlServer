using PlServer.Domain.Executables;
using System.Collections.Immutable;

namespace PlServer.Plugins.Loading;

public class PluginDescription
{
    public IPlugin Plugin { get; }

    public ImmutableArray<IExecutable> Executables { get; }

    public PluginDescription(IPlugin plugin, IEnumerable<IExecutable> executables)
    {
        Plugin = plugin;
        Executables = executables.ToImmutableArray();
    }
}
