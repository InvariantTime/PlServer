
namespace PlServer.Worker.Process;

public readonly record struct ProcessId(Guid Id)
{
    public static ProcessId New() => new(Guid.NewGuid());
}
