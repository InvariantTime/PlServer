
namespace PlServer.Domain.Nodes.Events;

public record NodeAddedEvent(
    NodeGraphId GraphId, 
    long Version, 
    DateTime OccuredAt, 
    NodeId NodeId) : INodeGraphEvent;
