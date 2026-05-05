using PlServer.Domain;
using PlServer.Domain.Nodes;
using PlServer.Domain.Nodes.Events;
using System.Numerics;

namespace PlServer.Server.Infrastructure.NodeGraphs;

public abstract record SynchronizeSnapshot
{
    public long Version { get; }

    public abstract string Type { get; }

    public SynchronizeSnapshot(long version)
    {
        Version = version;
    }

    public static FullSynchronizeSnapshot CreateFullSync(long version)
    {
        NodeConnection[] connections = [
            new NodeConnection(
                new NodeConnectionPart(NodePinId.New(), NodeId.New()),
                new NodeConnectionPart(NodePinId.New(), NodeId.New()))
        ];

        NodeDescription[] nodes = [
            new NodeDescription(NodeId.New(), "vasya", "abc", new Vector2(100, 0)),
            new NodeDescription(NodeId.New(), "petya", "abc", new Vector2(0, 0)),
            new NodeDescription(NodeId.New(), "grisha", "abc", new Vector2(-100, 0)),
        ];

        return new FullSynchronizeSnapshot(version, nodes, connections);
    }

    public static DeltaSynchronizeSnapshot CreateDeltaSync(long version, IEnumerable<INodeGraphEvent> events)
    {
        var sorted = events.OrderBy(x => x.Version);
        return new DeltaSynchronizeSnapshot(version, sorted);
    }
}

public record FullSynchronizeSnapshot : SynchronizeSnapshot
{
    public override string Type => "full";

    public NodeConnection[] Connections { get; }

    public NodeDescription[] Nodes { get; }

    public FullSynchronizeSnapshot(long value, 
        IEnumerable<NodeDescription> nodes, 
        IEnumerable<NodeConnection> connections) : base(value)
    {
        Connections = connections.ToArray();
        Nodes = nodes.ToArray();
    }
}

public record DeltaSynchronizeSnapshot : SynchronizeSnapshot
{
    public override string Type => "delta";

    public INodeGraphEvent[] Events { get; }

    public DeltaSynchronizeSnapshot(long version, IEnumerable<INodeGraphEvent> events) : base(version)
    {
        Events = events.ToArray();
    }
}