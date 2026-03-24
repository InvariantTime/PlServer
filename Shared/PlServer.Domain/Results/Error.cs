
namespace PlServer.Domain.Results;

public readonly record struct Error<T>(T Name, string Description, Exception? Exception) where T : struct;

public static class Error
{
    public static readonly Error<ErrorTypes> None = new(ErrorTypes.None, string.Empty, null);

    public static readonly Error<ErrorTypes> Unknown = new(ErrorTypes.Unknown, string.Empty, null);

    public static readonly Error<ErrorTypes> Common = new(ErrorTypes.Common, string.Empty, null);

    public static Error<T> New<T>(T name) where T : struct
    {
        return new(name, string.Empty, null);
    }

    public static Error<T> New<T>(T name, string description, Exception? exception = null) where T : struct
    {
        return new(name, description, exception);
    }

    public static Error<T> Default<T>() where T : struct
    {
        return new(default, string.Empty, null);
    }
}

public enum ErrorTypes
{
    None,
    Unknown,
    Common
}