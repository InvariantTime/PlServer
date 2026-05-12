using PlServer.Domain.Nodes.Events;

namespace PlServer.Server.Infrastructure.NodeGraphs;

public interface INodeGraphNotifier
{
    Task NotifyEventAsync(INodeGraphEvent @event);
}
