
namespace PlServer.Server.Domain.Users;

public interface IReadOnlyUserCollection
{
    UserId HostId { get; }

    ICollection<User> Users { get; }

    int MaxUserCount { get; }

    bool HasHost { get; }

    int UserCount { get; }
}
