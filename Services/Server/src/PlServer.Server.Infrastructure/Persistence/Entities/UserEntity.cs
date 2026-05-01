
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Infrastructure.Persistence.Entities;

public class UserEntity
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public string PasswordHash { get; init; }

    public UserEntity(Guid id, string name, string passwordHash)
    {
        Id = id;
        Name = name;
        PasswordHash = passwordHash;
    }

    public User ToDomain()
    {
        return User.Create(new UserId(Id), Name, PasswordHash).Value!;
    }

    public static UserEntity FromDomain(User domain)
    {
        return new UserEntity(domain.Key.Id, domain.Name, domain.PasswordHash);
    }
}
