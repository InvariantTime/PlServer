using PlServer.Domain;
using PlServer.Domain.Results;
using PlServer.Server.Domain.Events;

namespace PlServer.Server.Domain.Users;

public class User : AggregateRoot<UserId>
{
    public string Name { get; }

    public string PasswordHash { get; }

    private User(string name, string passwordHash, UserId id) : base(id)
    {
        Name = name;
        PasswordHash = passwordHash;
    }

    public static Result<User> RegisterNewUser(UserId id, string name, string passwordHash)
    {
        var user = new User(name, passwordHash, id);
        user.AddEvent(new UserRegisteredEvent(id, name));

        return Result.Success(user);
    }
}