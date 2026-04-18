
namespace PlServer.Server.Domain.Users;

public class ReadOnlyUserCollection : IReadOnlyUserCollection
{
    private readonly IReadOnlyUserCollection _base;

    public UserId HostId => _base.HostId;

    public ICollection<UserId> Users => _base.Users;

    public int MaxUserCount => _base.MaxUserCount;

    public int UserCount => _base.UserCount;

    public bool HasHost => _base.HasHost;

    public ReadOnlyUserCollection(IReadOnlyUserCollection @base)
    {
        _base = @base;
    }
}
