using PlServer.Domain.Results;

namespace PlServer.Server.Domain;

public class UserCollection : IDisposable
{
    private readonly Dictionary<UserId, User> _users;
    private readonly SemaphoreSlim _locker;

    public UserId HostId { get; }

    public int MaxUserCount { get; }

    public int UserCount => GetUserCount();

    public bool HasHost => HasHostInternal();

    public ICollection<User> Users => CopyUsers();

    public UserCollection(UserId host, int maxUserCount)
    {
        MaxUserCount = maxUserCount;
        HostId = host;

        _users = new Dictionary<UserId, User>();
        _locker = new SemaphoreSlim(1, 1);
    }

    public UnitResult<SessionErrors> TryAdd(User user)
    {
        try
        {
            _locker.Wait();

            if (_users.Count >= MaxUserCount)
                return Result.Failure(SessionErrors.SessionFull);

            bool result = _users.TryAdd(user.Key, user);

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

            bool result = _users.Remove(id, out var user);

            isHost = user?.Key == HostId;
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
            return _users.ContainsKey(HostId);
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

    private IList<User> CopyUsers()
    {
        try
        {
            _locker.Wait();
            return _users.Values.ToList();
        }
        finally
        {
            _locker.Release();
        }
    }
}
