
namespace PlServer.Domain;

public readonly struct Result
{
    public bool IsSuccess { get; init; }

    public Error Error { get; init; }

    public static Result Success()
    {
        return new Result()
        {
            IsSuccess = true,
            Error = Domain.Error.None
        };
    }

    public static Result Failure(Error error)
    {
        return new Result
        {
            IsSuccess = false,
            Error = error
        };
    }

    public static Result Failure(string errorName)
    {
        return Failure(Error.New(errorName));
    }

    public static Result Failure(string errorName, string errorDescription)
    {
        return Failure(Error.New(errorName, errorDescription));
    }

    public static Result<T> Success<T>(T value)
    {
        return new Result<T>()
        {
            IsSuccess = true,
            Value = value,
            Error = Domain.Error.None
        };
    }

    public static Result<T> Failure<T>(Error error)
    {
        return new Result<T>
        {
            IsSuccess = false,

            Error = error
        };
    }

    public static Result<T> Failure<T>(string errorName)
    {
        return Failure<T>(Error.New(errorName));
    }

    public static Result<T> Failure<T>(string errorName, string errorDescription)
    {
        return Failure<T>(Error.New(errorName, errorDescription));
    }
}

public readonly struct Result<T>
{
    public bool IsSuccess { get; init; }

    public T? Value { get; init; }

    public Error Error { get; init; }
}