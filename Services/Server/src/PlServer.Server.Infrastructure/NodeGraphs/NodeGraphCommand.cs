using PlServer.Domain.Nodes;
using PlServer.Server.Services.DTOs;
using System.Text.Json.Serialization;

namespace PlServer.Server.Infrastructure.NodeGraphs;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(AddNodeCommand), "add_node")]
[JsonDerivedType(typeof(RemoveNodeCommand), "remove_node")]
[JsonDerivedType(typeof(MoveNodeCommand), "move_node")]
[JsonDerivedType(typeof(AddConnectionCommand), "add_connection")]
[JsonDerivedType(typeof(RemoveConnectionCommand), "remove_connection")]
public abstract record NodeGraphCommand;

public record AddNodeCommand(Point Position, string Definition) : NodeGraphCommand;

public record RemoveNodeCommand(NodeId NodeId) : NodeGraphCommand;

public record MoveNodeCommand(NodeId NodeId, Point Position) : NodeGraphCommand;

public record AddConnectionCommand(NodeConnection Connection) : NodeGraphCommand;

public record RemoveConnectionCommand(NodeConnectionPart Target) : NodeGraphCommand;