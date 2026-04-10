
namespace PlServer.Domain;

public interface IEventSource<out T> where T : class, IDomainEvent
{
    IReadOnlyCollection<T> Events { get; }

    IReadOnlyCollection<T> PullEvents();
}