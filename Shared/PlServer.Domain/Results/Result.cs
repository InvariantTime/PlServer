namespace PlServer.Domain.Results;

public readonly partial struct Result : IResult<Error<ErrorTypes>>
{
    public bool IsSuccess { get; init; }

    public Error<ErrorTypes> Error { get; init; }

    public static Result Success()
    {
        return new Result()
        {
            IsSuccess = true,
            Error = Results.Error.None
        };
    }

    public static Result Failure(Error<ErrorTypes> error)
    {
        return new Result
        {
            IsSuccess = false,
            Error = error
        };
    }

    public static Result Failure(ErrorTypes error)
    {
        return Failure(error);
    }

    public static Result Failure(ErrorTypes error, string errorDescription)
    {
        return Failure(Results.Error.New(error, errorDescription));
    }

    public static Result<T> Success<T>(T value)
    {
        return new Result<T>()
        {
            IsSuccess = true,
            Value = value,
            Error = Results.Error.None
        };
    }

    public static Result<T> Failure<T>(Error<ErrorTypes> error)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Error = error
        };
    }

    public static Result<T> Failure<T>(ErrorTypes error)
    {
        return Failure<T>(Results.Error.New(error));
    }

    public static Result<T> Failure<T>(ErrorTypes error, string errorDescription)
    {
        return Failure<T>(Results.Error.New(error, errorDescription));
    }

    public static UnitResult<TError> Success<TError>() where TError : struct
    {
        return new UnitResult<TError>()
        {
            IsSuccess = true,
            Error = Results.Error.Default<TError>()
        };
    }

    public static UnitResult<TError> Failure<TError>(TError error) where TError : struct
    {
        return new UnitResult<TError>()
        {
            IsSuccess = false,
            Error = Results.Error.New(error, string.Empty)
        };
    }

    public static UnitResult<TError> Failure<TError>(TError error, string description) where TError : struct
    {
        return new UnitResult<TError>()
        {
            IsSuccess = false,
            Error = Results.Error.New(error, description)
        };
    }

    public static UnitResult<TError> Failure<TError>(Error<TError> error) where TError : struct
    {
        return new UnitResult<TError>()
        {
            IsSuccess = false,
            Error = error
        };
    }

    public static Result<TError, TResult> Success<TError, TResult>(TResult success) where TError : struct
    {
        return new Result<TError, TResult>()
        {
            IsSuccess = true,
            Value = success,
            Error = Results.Error.Default<TError>()
        };
    }

    public static Result<TError, TResult> Failure<TError, TResult>(TError error) where TError : struct
    {
        return new Result<TError, TResult>()
        {
            IsSuccess = false,
            Error = Results.Error.New(error)
        };
    }

    public static Result<TError, TResult> Failure<TError, TResult>(TError error, string description) where TError : struct
    {
        return new Result<TError, TResult>()
        {
            IsSuccess = false,
            Error = Results.Error.New(error, description)
        };
    }

    public static Result<TError, TResult> Failure<TError, TResult>(Error<TError> error) where TError : struct
    {
        return new Result<TError, TResult>()
        {
            IsSuccess = false,
            Error = error
        };
    }
}