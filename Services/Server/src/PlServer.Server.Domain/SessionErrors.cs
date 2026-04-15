
namespace PlServer.Server.Domain;

public enum SessionErrors
{
    None = default,
    Common = 1,
    UserAlreadyExists = 2,
    UserNotExists = 3,
    SessionFull = 4
}
