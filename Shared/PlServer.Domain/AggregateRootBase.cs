
namespace PlServer.Domain;

public abstract class AggregateRootBase<TKey, TEvent> : Entity<TKey>, IEventSource<TEvent>
    where TKey : notnull
    where TEvent : IDomainEvent
{
    public abstract IReadOnlyCollection<TEvent> Events { get; }

    protected AggregateRootBase(TKey id) : base(id)
    {
    }

    public abstract IReadOnlyCollection<TEvent> PullEvents();
}