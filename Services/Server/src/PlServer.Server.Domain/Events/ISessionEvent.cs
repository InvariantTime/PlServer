
using PlServer.Domain;

namespace PlServer.Server.Domain.Events;

public interface ISessionEvent : IDomainEvent
{
    SessionId SessionId { get; }
}
