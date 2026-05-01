
using PlServer.Application;

namespace PlServer.Infrastructure.Events;

public interface IEventDispatcherFactory
{
    IEventDispatcher CreateDispatcher(IServiceProvider scope);
}
