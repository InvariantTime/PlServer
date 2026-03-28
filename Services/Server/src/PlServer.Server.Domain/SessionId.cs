
namespace PlServer.Server.Domain;

public readonly record struct SessionId(Guid Id)
{
    public static readonly SessionId Empty = new SessionId(Guid.Empty);

    public static SessionId New() => new SessionId(Guid.NewGuid());
}
