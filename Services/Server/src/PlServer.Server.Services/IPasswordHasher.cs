
namespace PlServer.Server.Services;

public interface IPasswordHasher
{
    string Hash(string value);

    bool Verify(string password, string passwordHash);
}
