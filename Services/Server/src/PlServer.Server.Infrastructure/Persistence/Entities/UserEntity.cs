
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Infrastructure.Persistence.Entities;

public class UserEntity
{
    public Guid Id { get; }

    public string Name { get; }

    public string PasswordHash { get; }

    public UserEntity(Guid id, string name, string passwordHash)
    {
        Id = id;
        Name = name;
        PasswordHash = passwordHash;
    }

    public User Map()
    {
        return User.Create(new UserId(Id), Name, PasswordHash).Value!;
    }
}
