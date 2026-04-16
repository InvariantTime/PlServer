
namespace PlServer.Domain.Nodes.Events;

public interface INodeGraphEvent : IDomainEvent
{
    NodeGraphId GraphId { get; }
}
