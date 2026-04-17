namespace PlServer.Server.Domain.Users;

public readonly record struct UserId(Guid Id)
{
    public static UserId New() => new UserId(Guid.NewGuid());
}