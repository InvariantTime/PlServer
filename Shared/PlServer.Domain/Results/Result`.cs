
namespace PlServer.Domain.Results;

public readonly struct Result<T> : IResult<Error<ErrorTypes>, T>
{
    public bool IsSuccess { get; init; }

    public T? Value { get; init; }

    public Error<ErrorTypes> Error { get; init; }
}

public readonly struct Result<TError, TResult> : IResult<Error<TError>, TResult> where TError : struct
{
    public TResult? Value { get; init; }

    public bool IsSuccess { get; init; }

    public Error<TError> Error { get; init; }
}

public readonly struct UnitResult<TError> : IResult<Error<TError>> where TError : struct
{
    public bool IsSuccess { get; init; }

    public Error<TError> Error { get; init; }
}