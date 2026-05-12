using PlServer.Application;
using PlServer.Domain.Nodes.Events;
using PlServer.Server.Infrastructure.NodeGraphs;

namespace PlServer.Server.Infrastructure.Handlers.Sessions;

public class NodeGraphEventHandler : IDomainEventHandler<INodeGraphEvent>
{
    private readonly INodeGraphNotifier _notifier;

    public NodeGraphEventHandler(INodeGraphNotifier notifier)
    {
        _notifier = notifier;
    }

    public Task HandleAsync(INodeGraphEvent @event, CancellationToken cancellation)
    {
        return _notifier.NotifyEventAsync(@event);
    }
}
