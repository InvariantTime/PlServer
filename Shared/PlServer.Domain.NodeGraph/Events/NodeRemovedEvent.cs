
namespace PlServer.Domain.Nodes.Events;

public record NodeRemovedEvent(
    NodeGraphId GraphId, 
    long Version, 
    DateTime OccuredAt, 
    NodeId NodeId) : INodeGraphEvent;