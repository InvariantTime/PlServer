
namespace PlServer.Worker.API.Services;

public class WorkerStartupService : BackgroundService
{
    private readonly ILogger<WorkerStartupService> _logger;

    public WorkerStartupService(ILogger<WorkerStartupService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

        while (await timer.WaitForNextTickAsync(stoppingToken) == true)
        {
            _logger.LogInformation("Work!");
        }
    }
}
