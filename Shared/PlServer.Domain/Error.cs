
namespace PlServer.Domain;

public readonly record struct Error(string Name, string Description, Exception? Exception)
{
    public static readonly Error None = new(string.Empty, string.Empty, null);

    public static readonly Error Unknown = new("Unknown", string.Empty, null);

    public static Error New(string name)
    {
        return new(name, string.Empty, null);
    }

    public static Error New(string name, string description, Exception? exception = null)
    {
        return new(name, description, exception);
    }
}