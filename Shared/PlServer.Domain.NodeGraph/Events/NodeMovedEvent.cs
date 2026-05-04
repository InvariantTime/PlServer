using System.Numerics;

namespace PlServer.Domain.Nodes.Events;

public record NodeMovedEvent(
    NodeGraphId GraphId, 
    long Version, 
    DateTime OccuredAt, 
    NodeId NodeId, 
    Vector2 NewPosition) : INodeGraphEvent;