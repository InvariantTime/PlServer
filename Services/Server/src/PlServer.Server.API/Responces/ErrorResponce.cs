using PlServer.Domain.Results;

namespace PlServer.Server.API.Responces;

public record ErrorResponce(string Error);

public static class ErrorResponceExtensions
{
    public static ErrorResponce AsErrorResponce<T>(this IResult<Error<T>> result) where T : struct 
    {
        return new ErrorResponce(result.Error.Description);
    }
}