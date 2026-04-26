
namespace PlServer.Server.Domain;

public readonly record struct SessionId(Guid Id)
{
    public static readonly SessionId Empty = new SessionId(Guid.Empty);

    public static SessionId New() => new SessionId(Guid.NewGuid());

    public static bool TryParse(string? value, out SessionId id)
    {
        id = default;

        if (value == null)
            return false;

        bool result = Guid.TryParse(value, out var guid);

        if (result == true)
        {
            id = new SessionId(guid);
            return true;
        }

        return false;
    }

    public static implicit operator Guid(SessionId id)
    {
        return id.Id;
    }
}
