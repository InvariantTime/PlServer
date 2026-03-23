
using PlServer.Domain;

namespace PlServer.Nodes;

public class NodeConnection : ValueObject
{
    public Guid Source { get; }

    public Guid Target { get; }

    protected override IEnumerable<object> GetComponents()
    {
        return [Source, Target];
    }
}
