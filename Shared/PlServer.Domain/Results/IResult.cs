namespace PlServer.Domain.Results;

public interface IResult<TError> where TError : struct
{
    bool IsSuccess { get; }

    TError Error { get; }
}

public interface IResult<TError, TResult> : IResult<TError> where TError : struct
{
    TResult? Value { get; }
}
