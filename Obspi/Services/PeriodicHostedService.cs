
namespace Obspi.Services;

public abstract class PeriodicHostedService : BackgroundService
{
    private readonly ILogger<PeriodicHostedService> _logger;
    private readonly IServiceScopeFactory _factory;

    protected PeriodicHostedService(
        ILogger<PeriodicHostedService> logger,
        IServiceScopeFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(Period);

        while (!stoppingToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                if (IsEnabled)
                {
                    await using var scope = _factory.CreateAsyncScope();
                    await OnExecute(scope, stoppingToken);
                }
            }
            catch (OperationCanceledException) { } // ignore
            catch (Exception e)
            {
                _logger.LogError(e, "Exception in periodic hosted service");
            }
        }
    }

    public bool IsEnabled { get; set; }

    public TimeSpan Period { get; set; } = TimeSpan.FromMinutes(1);

    protected abstract Task OnExecute(AsyncServiceScope scope, CancellationToken stoppingToken);
}
