using PlServer.Application;
using PlServer.Domain;

namespace PlServer.Infrastructure.Events;

public class EventDispatcher : IEventDispatcher
{
    private readonly IEventLauncherSource _source;
    private readonly IServiceProvider _scope;

    public EventDispatcher(IEventLauncherSource source, IServiceProvider scope)
    {
        _source = source;
        _scope = scope;
    }

    public Task DispatchEventAsync(IDomainEvent @event, CancellationToken cancellation = default)
    {
        var launcher = _source.GetOrCreateLauncher(@event.GetType());
        return launcher.Invoke(@event, _scope, cancellation);
    }
}
