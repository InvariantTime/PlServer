using PlServer.Domain.Results;

namespace PlServer.Server.Domain.Users;

public class UserCollection : IDisposable, IReadOnlyUserCollection
{
    private readonly HashSet<UserId> _users;
    private readonly SemaphoreSlim _locker;

    public UserId HostId { get; }

    public int MaxUserCount { get; }

    public int UserCount => GetUserCount();

    public bool HasHost => HasHostInternal();

    public ICollection<UserId> Users => CopyUsers();

    public UserCollection(UserId host, int maxUserCount)
    {
        MaxUserCount = maxUserCount;
        HostId = host;

        _users = new HashSet<UserId>();
        _locker = new SemaphoreSlim(1, 1);
    }

    public UnitResult<SessionErrors> TryAdd(UserId user)
    {
        try
        {
            _locker.Wait();

            if (_users.Count >= MaxUserCount)
                return Result.Failure(SessionErrors.SessionFull);

            bool result = _users.Add(user);

            return Result.Check(result == true, SessionErrors.UserAlreadyExists);
        }
        finally
        {
            _locker.Release();
        }
    }

    public UnitResult<SessionErrors> TryRemove(UserId id, out bool isHost)
    {
        try
        {
            _locker.Wait();

            bool result = _users.Remove(id);

            isHost = id == HostId;
            return Result.Check(result == true, SessionErrors.UserNotExists);
        }
        finally
        {
            _locker.Release();
        }
    }

    public void Dispose()
    {
        _users.Clear();
        _locker.Dispose();
    }

    private bool HasHostInternal()
    {
        try
        {
            _locker.Wait();
            return _users.Contains(HostId);
        }
        finally
        {
            _locker.Release();
        }
    }

    private int GetUserCount()
    {
        try
        {
            _locker.Wait();
            return _users.Count;
        }
        finally
        {
            _locker.Release();
        }
    }

    private IList<UserId> CopyUsers()
    {
        try
        {
            _locker.Wait();
            return _users.ToList();
        }
        finally
        {
            _locker.Release();
        }
    }
}
