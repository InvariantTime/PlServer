
namespace PlServer.Domain;

public abstract class Entity<TKey> : IEquatable<Entity<TKey>> where TKey : notnull
{
    public TKey Key { get; }

    protected Entity(TKey key)
    {
        Key = key;
    }

    public bool Equals(Entity<TKey>? other)
    {
        if (ReferenceEquals(this, other) == true)
            return true;

        if (other == null || GetType() != other.GetType())
            return false;

        return Key.Equals(other.Key);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Entity<TKey>);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), Key);
    }

    public override string ToString()
    {
        return $"{GetType()} [{Key}]";
    }

    public static bool operator ==(Entity<TKey>? left, Entity<TKey>? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(Entity<TKey>? left, Entity<TKey>? right)
    {
        return !(left == right);
    }
}
