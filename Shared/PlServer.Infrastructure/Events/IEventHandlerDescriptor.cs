
namespace PlServer.Infrastructure.Events;

public interface IEventHandlerDescriptor
{
    public Type HandlerType { get; }

    bool IsSupporting(Type eventType);
}
