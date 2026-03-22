
namespace PlServer.Domain;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public bool Equals(ValueObject? other)
    {
        if (other is null || GetType() != other.GetType())
            return false;

        var components = GetComponents();
        var otherComponents = GetComponents();

        return components.SequenceEqual(otherComponents);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ValueObject);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        var components = GetComponents();

        foreach (var component in components)
            hash.Add(component.GetHashCode());

        return hash.ToHashCode();
    }

    protected abstract IEnumerable<object> GetComponents();

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }
}