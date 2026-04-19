
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Services.Repositories;

public interface IUserRepository
{
    bool AddUser(User user);

    bool RemoveUser(UserId id);

    User? GetById(UserId id);

    User? GetByName(string name);

    bool HasUserWithName(string name);

    ICollection<User> GetAll();
}
