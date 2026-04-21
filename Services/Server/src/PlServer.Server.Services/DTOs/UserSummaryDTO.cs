using PlServer.Server.Domain.Users;

namespace PlServer.Server.Services.DTOs;

public record class UserSummaryDTO(UserId Id, string Name);