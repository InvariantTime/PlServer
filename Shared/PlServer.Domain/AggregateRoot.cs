
namespace PlServer.Domain;

public class AggregateRoot<TKey, TEvent> : AggregateRootBase<TKey, TEvent>
    where TKey : notnull
    where TEvent : IDomainEvent
{
    private readonly List<TEvent> _events;

    public override IReadOnlyCollection<TEvent> Events => _events.AsReadOnly();

    protected AggregateRoot(TKey id) : base(id)
    {
        _events = new List<TEvent>();
    }

    protected void AddEvent(TEvent @event)
    {
        _events.Add(@event);
    }

    public override IReadOnlyCollection<TEvent> PullEvents()
    {
        var events = _events.AsReadOnly();
        _events.Clear();

        return events;
    }
}

public class AggregateRoot<TKey> : AggregateRoot<TKey, IDomainEvent> where TKey : notnull
{
    protected AggregateRoot(TKey id) : base(id)
    {
    }
}