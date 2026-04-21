using PlServer.Application;
using PlServer.Domain.Results;
using PlServer.Server.Domain.Users;
using PlServer.Server.Services.DTOs;
using PlServer.Server.Services.Repositories;

namespace PlServer.Server.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _hasher;
    private readonly IEventDispatcher _dispatcher;

    public UserService(IUserRepository users, IPasswordHasher hasher, IEventDispatcher dispatcher)
    {
        _users = users;
        _hasher = hasher;
        _dispatcher = dispatcher;
    }

    public async Task<Result<UserId>> LoginAsync(string name, string password)
    {
        var user = _users.GetByName(name);

        if (user == null)
            return Result.Failure<UserId>(ErrorTypes.Common, "Invalid name or password");

        if (_hasher.Verify(password, user.PasswordHash) == false)
            return Result.Failure<UserId>(ErrorTypes.Common, "Invalid name or password");

        await _dispatcher.DispatchEntityEventsAsync(user);

        return Result.Success(user.Key);
    }

    public async Task<Result<UserId>> RegisterAsync(string name, string password)
    {
        var hasOther = _users.HasUserWithName(name);

        if (hasOther == true)
            return Result.Failure<UserId>(ErrorTypes.Common, "User with such name already exists");

        var passwordHash = _hasher.Hash(password);

        var id = UserId.New();
        var user = User.RegisterNewUser(id, name, passwordHash);

        if (user.IsSuccess == false)
            return Result.Failure<UserId>(user.Error);

        var added = _users.AddUser(user.Value!);

        if (added == false)
            return Result.Failure<UserId>(ErrorTypes.Unknown, "server error");

        await _dispatcher.DispatchEntityEventsAsync(user.Value!);

        return Result.Success(id);
    }

    public UserSummaryDTO? GetUserById(UserId id)
    {
        var user = _users.GetAll().FirstOrDefault(x => x.Key == id);

        return user != null ? new UserSummaryDTO(user.Key, user.Name) : null;
    }
}
