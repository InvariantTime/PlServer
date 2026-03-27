
namespace PlServer.Server.Domain;

public readonly record struct SessionId(Guid Id)
{
    public static SessionId New() => new SessionId(Guid.NewGuid());
}
