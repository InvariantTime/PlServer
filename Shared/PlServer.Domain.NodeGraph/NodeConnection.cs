
namespace PlServer.Domain.Nodes;

public readonly record struct NodeConnection(NodePinId SourcePin, NodeId SourceNode, NodePinId TargetPin, NodeId TargetNode);