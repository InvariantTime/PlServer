namespace PlServer.Server.Domain.Users;

public readonly record struct UserId(Guid Id)
{
    public static UserId New() => new UserId(Guid.NewGuid());

    public static UserId? FromString(string id)
    {
        if (Guid.TryParse(id, out var guid) == false)
            return null;

        return new UserId(guid);
    }
}