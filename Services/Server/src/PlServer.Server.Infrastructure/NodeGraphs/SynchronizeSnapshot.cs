using PlServer.Domain;
using PlServer.Domain.Nodes;
using PlServer.Domain.Nodes.Events;
using PlServer.Server.Services.DTOs;
using System.Numerics;

namespace PlServer.Server.Infrastructure.NodeGraphs;

public abstract record SynchronizeSnapshot
{
    public long Version { get; }

    public abstract string Type { get; }

    public static SynchronizeSnapshot Empty => EmptySnapshot.Instance;

    public SynchronizeSnapshot(long version)
    {
        Version = version;
    }

    public static FullSynchronizeSnapshot CreateFullSync(
        IEnumerable<NodeSummaryDTO> nodes, 
        IEnumerable<NodeConnection> connections, 
        long version)
    {

        return new FullSynchronizeSnapshot(version, nodes, connections);
    }

    public static DeltaSynchronizeSnapshot CreateDeltaSync(long version, IEnumerable<INodeGraphEvent> events)
    {
        var sorted = events.OrderBy(x => x.Version);
        return new DeltaSynchronizeSnapshot(version, sorted);
    }
}

public record EmptySnapshot : SynchronizeSnapshot
{
    public static readonly EmptySnapshot Instance = new();

    public override string Type => "empty";

    private EmptySnapshot() : base(1)
    {
    }
}

public record FullSynchronizeSnapshot : SynchronizeSnapshot
{
    public override string Type => "full";

    public NodeConnection[] Connections { get; }

    public NodeSummaryDTO[] Nodes { get; }

    public FullSynchronizeSnapshot(long value, 
        IEnumerable<NodeSummaryDTO> nodes, 
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