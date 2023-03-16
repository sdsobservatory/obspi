using Obspi.Commands;

namespace Obspi;

public class ObservatoryService : BackgroundService
{
    private readonly ILogger<ObservatoryService> _logger;
    private readonly Observatory _observatory;

    public TimeSpan Interval => TimeSpan.FromMilliseconds(500);

    public ObservatoryService(
        ILogger<ObservatoryService> logger,
        Observatory observatory)
    {
        _logger = logger;
        _observatory = observatory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var loopTimeQueue = new Queue<double>(10);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Run(async () =>
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var start = DateTime.UtcNow;

                        Loop();

                        var elapsed = DateTime.UtcNow - start;
                        var next = start + Interval;
                        var remaining = next - DateTime.UtcNow;
                        if (remaining > TimeSpan.Zero)
                            await Task.Delay(remaining, stoppingToken);
                        
                        loopTimeQueue.Enqueue(elapsed.TotalSeconds);
                        _observatory.LoopTime = TimeSpan.FromSeconds(loopTimeQueue.Average());
                    }
                });
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (!stoppingToken.IsCancellationRequested)
                    await Task.Delay(1000);
            }
        }
    }

    private void Loop()
    {
        var outputs = _observatory.IO.Outputs.ToSnapshot();
        var inputs = _observatory.IO.Inputs.ToSnapshot();
        
        var roofSafeConditions = new[]
        {
            inputs.CloudWatcherSafe,
            inputs.Tilt1,
            inputs.Tilt2,
            inputs.Tilt3,
            inputs.Tilt4,
        };
        
        _observatory.CanRoofMove = roofSafeConditions.All(x => x);
        _observatory.IsRoofOpen = inputs is { RoofOpen: true, RoofClosed: false };
        _observatory.IsRoofClosed = inputs is { RoofOpen: false, RoofClosed: true };

        if (_observatory.Commands.TryPeek(out var command)
            && command.State == CommandState.Created)
        {
            Task.Run(() =>
            {
                Command localCommand = command;
                _logger.LogInformation("Running {Command}", command);
                localCommand.Run(_observatory);
                while (!_observatory.Commands.TryDequeue(out var _))
                {
                }
                _logger.LogInformation("Command complete");
            });
        }
    }
}
