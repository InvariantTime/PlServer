
using PlServer.Domain.Nodes;
using PlServer.Server.Services.DTOs;
using System.Text.Json.Serialization;

namespace PlServer.Server.Infrastructure.NodeGraphs;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(AddNodeCommand), "add_node")]
[JsonDerivedType(typeof(RemoveNodeCommand), "remove_node")]
public abstract record NodeGraphCommand;

public record AddNodeCommand(Point Position, string Definition) : NodeGraphCommand;

public record RemoveNodeCommand(NodeId Id) : NodeGraphCommand;