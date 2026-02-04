
namespace PlServer.Server.Infrastructure.Sessions;

public class Session
{
    public string Name { get; }

    public Guid Id { get; }

    public Session(string name, Guid id)
    {
        Name = name;
        Id = id;
    }
}
