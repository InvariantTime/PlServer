
namespace PlServer.Domain;

public class AggregateRoot<TKey> : Entity<TKey> where TKey : notnull
{
    private readonly List<IDomainEvent> _events;

    public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

    protected AggregateRoot(TKey id) : base(id)
    {
        _events = new List<IDomainEvent>();
    }

    protected void AddEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }

    public IReadOnlyCollection<IDomainEvent> PullEvents()
    {
        var events = _events.AsReadOnly();
        _events.Clear();

        return events;
    }
}