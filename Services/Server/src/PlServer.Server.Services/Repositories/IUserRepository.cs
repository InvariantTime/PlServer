
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Services.Repositories;

public interface IUserRepository
{
    Task<bool> AddUserAsync(User user);

    Task<User?> RemoveUserAsync(UserId id);

    Task<User?> GetByIdAsync(UserId id);

    Task<User?> GetByNameAsync(string name);

    Task<bool> HasUserWithNameAsync(string name);

    ICollection<User> GetAll();
}
