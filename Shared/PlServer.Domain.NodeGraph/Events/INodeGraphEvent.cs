
namespace PlServer.Domain.Nodes.Events;

public interface INodeGraphEvent : IDomainEvent
{
    NodeGraphId GraphId { get; }

    DateTime OccuredAt { get; }

    long Version { get; }
}
