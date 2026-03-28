using PlServer.Server.Domain;

namespace PlServer.Server.API.Responces;

public record SessionResponse(string Name, SessionId Id);