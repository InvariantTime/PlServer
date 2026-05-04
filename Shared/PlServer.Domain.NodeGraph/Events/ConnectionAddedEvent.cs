
namespace PlServer.Domain.Nodes.Events;

public record ConnectionAddedEvent(
    NodeGraphId GraphId, 
    long Version, 
    DateTime OccuredAt,
    NodeConnection Connection) : INodeGraphEvent;