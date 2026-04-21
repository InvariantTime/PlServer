namespace PlServer.Server.Infrastructure.Auth;

public sealed class AuthOptions
{
    public string UserIdClaim { get; init; } = string.Empty;

    public string SecretKey { get; init; } = string.Empty;

    public double ExpitesHours { get; init; } = 1;
}
