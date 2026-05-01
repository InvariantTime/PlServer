
using Microsoft.EntityFrameworkCore;
using PlServer.Server.Domain.Users;
using PlServer.Server.Infrastructure.Persistence;
using PlServer.Server.Infrastructure.Persistence.Entities;
using PlServer.Server.Services.Repositories;

namespace PlServer.Server.Infrastructure.Repositories;

public class EfUserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public EfUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddUserAsync(User user)
    {
        var entity = UserEntity.FromDomain(user);
        try
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public ICollection<User> GetAll()
    {
        return _context.Users.Select(x => x.ToDomain()).ToList();
    }

    public async Task<User?> GetByIdAsync(UserId id)
    {
        var result = await _context.Users.FindAsync(id.Id);

        if (result == null)
            return null;

        return result.ToDomain();
    }

    public async Task<User?> GetByNameAsync(string name)
    {
        var result = await _context.Users.FirstOrDefaultAsync(x => x.Name == name);

        if (result == null)
            return null;

        return result.ToDomain();
    }

    public async Task<bool> HasUserWithNameAsync(string name)
    {
        var result = await _context.Users.FirstOrDefaultAsync(x => x.Name == name);

        if (result == null)
            return false;

        return true;
    }

    public async Task<User?> RemoveUserAsync(UserId id)
    {
        var entity = await _context.Users.FindAsync(id.Id);

        if (entity == null)
            return null;

        try
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();

            return entity.ToDomain();
        }
        catch
        {
            return null;
        }
    }
}
