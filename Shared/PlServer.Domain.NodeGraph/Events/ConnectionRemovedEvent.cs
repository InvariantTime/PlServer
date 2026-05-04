
namespace PlServer.Domain.Nodes.Events;

public record ConnectionRemovedEvent(
    NodeGraphId GraphId, 
    long Version, 
    DateTime OccuredAt, 
    NodeConnectionPart Target) : INodeGraphEvent;