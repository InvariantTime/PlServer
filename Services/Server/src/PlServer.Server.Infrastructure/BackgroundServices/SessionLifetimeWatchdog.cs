using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlServer.Server.Infrastructure.Sessions;
using PlServer.Server.Services;

namespace PlServer.Server.Infrastructure.BackgroundServices;

public class SessionLifetimeWatchdog : BackgroundService
{
    private static readonly TimeSpan _sessionTimeout = TimeSpan.FromSeconds(30);
    private static readonly TimeSpan _checkPeriod = TimeSpan.FromSeconds(20); //TODO: move it to options ?

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly SessionWatchdogTracker _tracker;

    public SessionLifetimeWatchdog(IServiceScopeFactory factory, SessionWatchdogTracker tracker)
    {
        _scopeFactory = factory;
        _tracker = tracker;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(_checkPeriod);

        while (stoppingToken.IsCancellationRequested == false)
        {
            await timer.WaitForNextTickAsync(stoppingToken);
            await CheckSessionsAsync();
        }
    }

    private async Task CheckSessionsAsync()
    {
        var now = DateTime.UtcNow;
        var outdated = _tracker.Times
            .Where(x => now - x.Value >= _sessionTimeout);

        if (outdated.Any() == false)
            return;

        using var scope = _scopeFactory.CreateScope();

        var service = scope.ServiceProvider.GetRequiredService<ISessionService>();

        foreach (var session in outdated)
        {
            _tracker.RemoveSession(session.Key);
            await service.DeleteSessionAsync(session.Key);
        }
    }
}
