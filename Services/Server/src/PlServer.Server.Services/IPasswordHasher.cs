
namespace PlServer.Server.Services;

public interface IPasswordHasher
{
    string Hash(object value);

    bool IsValid(object password, string passwordHash);
}
