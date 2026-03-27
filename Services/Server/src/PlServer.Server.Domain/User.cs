
using PlServer.Domain;

namespace PlServer.Server.Domain;

public class User : Entity<UserId>
{
    public string Name { get; }

    public User(string name, UserId id) : base(id)
    {
        Name = name;
    }
}