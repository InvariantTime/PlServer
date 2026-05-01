
using Microsoft.Extensions.DependencyInjection;
using PlServer.Infrastructure.Events;
using PlServer.Server.Domain.Events;
using PlServer.Server.Infrastructure.Handlers.Lobby;
using PlServer.Server.Infrastructure.Handlers.Sessions;

namespace PlServer.Server.Infrastructure;

public static class EventHandlerServicesExtensions
{
    public static IServiceCollection AddEventHandling(this IServiceCollection services)
    {
        services.AddEventDispatching(builder =>
        {
            builder.AddGenericHandler<ISessionEvent, SessionLobbyEventHandler>();
            builder.AddHandler<SessionClosedEvent, SessionShutdownEventHandler>();
        });

        return services;
    }
}
