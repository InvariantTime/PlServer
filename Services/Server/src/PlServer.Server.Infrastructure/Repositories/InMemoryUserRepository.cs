using PlServer.Server.Domain.Users;
using PlServer.Server.Services.Repositories;
using System.Collections.Concurrent;

namespace PlServer.Server.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository //TODO: temporary, after need to use persistence db
{
    private readonly ConcurrentDictionary<UserId, User> _users;

    public InMemoryUserRepository()
    {
        _users = new ConcurrentDictionary<UserId, User>();
    }

    public bool AddUser(User user)
    {
        return _users.TryAdd(user.Key, user);
    }

    public ICollection<User> GetAll()
    {
        return _users.Values;
    }

    public User? GetById(UserId id)
    {
        _users.TryGetValue(id, out var user);
        return user;
    }

    public User? GetByName(string name)
    {
        return _users.Values.FirstOrDefault(x => x.Name == name);
    }

    public bool HasUserWithName(string name)
    {
        return _users.Values.FirstOrDefault(x => x.Name == name) != null;
    }

    public bool RemoveUser(UserId id, out User? user)
    {
        return _users.TryRemove(id, out user);
    }
}
