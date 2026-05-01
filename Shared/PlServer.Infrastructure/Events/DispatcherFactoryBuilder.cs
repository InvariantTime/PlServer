
namespace PlServer.Infrastructure.Events;

public class DispatcherFactoryBuilder
{
    private readonly List<IEventHandlerDescriptor> _descriptors = new();

    public IReadOnlyCollection<IEventHandlerDescriptor> Descriptors => _descriptors.AsReadOnly();

    public void AddDescriptor(IEventHandlerDescriptor descriptor)
    {
        _descriptors.Add(descriptor);
    }

    public IEventDispatcherFactory Build()
    {
        return new DefaultEventDispatcherFactory(_descriptors);
    }
}
