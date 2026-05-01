
namespace PlServer.Server.API.Responces;

public record SessionResponce
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }
    
    public required string HostName { get; init; }
    
    public required int MaxUserCount { get; init; }
    
    public required int UserCount { get; init; }
}