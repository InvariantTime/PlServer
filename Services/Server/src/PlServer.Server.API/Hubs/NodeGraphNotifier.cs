using Microsoft.AspNetCore.SignalR;
using PlServer.Domain;
using PlServer.Domain.Nodes.Events;
using PlServer.Server.Infrastructure.NodeGraphs;
using PlServer.Server.Infrastructure.Sessions;

namespace PlServer.Server.API.Hubs;

public class NodeGraphNotifier : INodeGraphNotifier
{
    private readonly IHubContext<SessionHub, ISessionClient> _context;
    private readonly ISessionConnectionTracker _tracker;

    public NodeGraphNotifier(IHubContext<SessionHub, ISessionClient> context, ISessionConnectionTracker tracker)
    {
        _context = context;
        _tracker = tracker;
    }

    public Task NotifyEventAsync(INodeGraphEvent @event)
    {
        var session = _tracker.GetSessionIdByGraph(@event.GraphId);

        if (session == null)
            return Task.CompletedTask;

        return _context.Clients.Group(session.ToString() ?? string.Empty).SendEventAsync(@event);
    }
}
