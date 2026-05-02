
namespace PlServer.Server.Domain.Events;

public record SessionConfirmedEvent(SessionId SessionId) : ISessionEvent;
