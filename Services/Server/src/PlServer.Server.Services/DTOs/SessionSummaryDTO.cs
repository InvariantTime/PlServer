
using PlServer.Domain.Nodes;
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Services.DTOs;

public record SessionSummaryDTO(SessionId Id, NodeGraphId NodeGraph, string Name, IReadOnlyUserCollection Users);
