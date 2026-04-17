using Microsoft.Extensions.DependencyInjection;
using PlServer.Application;
using PlServer.Server.Domain.Events;
using PlServer.Server.Infrastructure.Handlers.Sessions;

namespace PlServer.Server.Infrastructure;

public static class InfrastructureExtensions
{
    public static void RegisterHandlers(this IServiceCollection services)//TODO: scoped
    {
        RegisterEventDispatcher(services);

        services.AddSingleton<IDomainEventHandler<ISessionEvent>, SessionLobbyEventHandler>();
    }

    private static void RegisterEventDispatcher(IServiceCollection services)
    {
        services.AddSingleton<IEventDispatcher>((scope) => //TODO: event dispatcher builder
        {
            var handler = scope.GetService<IDomainEventHandler<ISessionEvent>>();

            EventHandlerLauncher launcher = (@event, cancellation) =>
            {
                if (@event is not ISessionEvent session)
                    return Task.CompletedTask;

                return handler?.HandleAsync(session) ?? Task.CompletedTask;
            };

            var dispatcher = new EventDispatcher(new Dictionary<Type, EventHandlerLauncher>
            {
                [typeof(SessionCreatedEvent)] = launcher,
                [typeof(UserJoinedEvent)] = launcher,
                [typeof(UserLeftEvent)] = launcher,
                [typeof(HostLeftEvent)] = launcher,
            });

            return dispatcher;
        });
    }
}
