
using PlServer.Server.Domain;
using PlServer.Server.Domain.Users;

namespace PlServer.Server.Services.DTOs;

public record SessionSummaryDTO(SessionId Id, string Name, UserId HostId, int MaxUsersCount, int UsersCount);
