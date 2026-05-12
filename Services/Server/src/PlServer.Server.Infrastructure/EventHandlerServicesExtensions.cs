using Microsoft.Extensions.DependencyInjection;
using PlServer.Domain.Nodes.Events;
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
            builder.AddGenericHandler<ISessionEvent, SessionLobbyEventHandler>()
                .AddBlackListed<SessionCreatedEvent>();

            builder.AddHandler<SessionClosedEvent, SessionShutdownEventHandler>();

            builder.AddMultipleHandler<SessionLifetimeWatchdogHandler>()
                .AddEventType<SessionCreatedEvent>()
                .AddEventType<SessionConfirmedEvent>();

            builder.AddGenericHandler<INodeGraphEvent, NodeGraphEventHandler>();
        });

        return services;
    }
}
