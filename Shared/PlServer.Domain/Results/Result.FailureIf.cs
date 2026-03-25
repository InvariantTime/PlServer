
namespace PlServer.Domain.Results;

public readonly partial struct Result
{
    public static Result FailureIf(bool condition, ErrorTypes error)
    {
        if (condition == true)
            return Success();

        return Failure(error);
    }

    public static Result FailureIf(bool condition, ErrorTypes error, string description)
    {
        if (condition == true)
            return Success();

        return Failure(error, description);
    }

    public static Result FailureIf(bool condition, Error<ErrorTypes> error)
    {
        if (condition == true)
            return Success();

        return Failure(error);
    }

    public static UnitResult<TError> FailureIf<TError>(bool condition, TError error) where TError : struct
    {
        if (condition == true)
            return Success<TError>();

        return Failure(error);
    }

    public static UnitResult<TError> FailureIf<TError>(bool condition, TError error, string description) where TError : struct
    {
        if (condition == true)
            return Success<TError>();

        return Failure(error, description);
    }

    public static UnitResult<TError> FailureIf<TError>(bool condition, Error<TError> error) where TError : struct
    {
        if (condition == true)
            return Success<TError>();

        return Failure(error);
    }
}
