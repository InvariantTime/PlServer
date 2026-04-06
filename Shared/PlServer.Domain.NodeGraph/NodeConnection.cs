
namespace PlServer.Domain.Nodes;


public record NodeConnection(NodeConnectionPart Source, NodeConnectionPart Target);

public readonly record struct NodeConnectionPart(NodePinId Pin, NodeId Node);