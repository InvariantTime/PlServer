
namespace PlServer.Domain;

public class AggregateRoot<TKey> : Entity<TKey> where TKey : notnull
{
    protected AggregateRoot(TKey id) : base(id)
    { }
}