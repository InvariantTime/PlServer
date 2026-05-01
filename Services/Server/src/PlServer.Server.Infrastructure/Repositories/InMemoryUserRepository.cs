
using PlServer.Server.Domain.Users;
using PlServer.Server.Services.Repositories;
using System.Collections.Concurrent;

namespace PlServer.Server.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<UserId, User> _users = new();

    public Task<bool> AddUserAsync(User user)
    {
        bool result = _users.TryAdd(user.Key, user);
        return Task.FromResult(result);
    }

    public ICollection<User> GetAll()
    {
        return _users.Values;
    }

    public Task<User?> GetByIdAsync(UserId id)
    {
        _users.TryGetValue(id, out var user);
        return Task.FromResult(user);
    }

    public Task<User?> GetByNameAsync(string name)
    {
        var result = _users.Values.FirstOrDefault(x => x.Name == name);
        return Task.FromResult(result);
    }

    public Task<bool> HasUserWithNameAsync(string name)
    {
        var result = _users.Values.FirstOrDefault(x => x.Name == name);
        return Task.FromResult(result != null);
    }

    public Task<User?> RemoveUserAsync(UserId id)
    {
        _users.TryRemove(id, out var user);
        return Task.FromResult(user);
    }
}
