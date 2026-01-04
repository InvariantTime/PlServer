using System.Reflection;
using System.Runtime.Loader;

namespace PlServer.Plugins.Loading;

public class PluginLoadContext : AssemblyLoadContext
{
    private readonly AssemblyDependencyResolver _resolver;

    public PluginLoadContext(string path)
    {
        _resolver = new AssemblyDependencyResolver(path);
    }

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        var path = _resolver.ResolveAssemblyToPath(assemblyName);

        if (path != null)
            return LoadFromAssemblyPath(path);

        return null;
    }
}
