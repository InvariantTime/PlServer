namespace PlServer.Plugins.Loading;

public interface IPluginLoader
{
    PluginDescription? LoadPlugin(string name);
}
