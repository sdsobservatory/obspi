using Obspi.Common;

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

                        Loop(stoppingToken);

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
                {
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }
    }

    private void Loop(CancellationToken token)
    {
        var outputs = _observatory.IO.Outputs.ToSnapshot();
        var inputs = _observatory.IO.Inputs.ToSnapshot();
        
        var isRoofSafeToMoveConditions = new[]
        {
            !inputs.CloudWatcherUnsafe,
            // TODO: uncomment when tilt switches work!
            //inputs.TiltJosh,
            //inputs.TiltAlex,
        };

		_observatory.IsRoofSafeToMove = isRoofSafeToMoveConditions.All(x => x);
		_observatory.IsRoofOpen = inputs is { RoofOpened: true, RoofClosed: false };
        _observatory.IsRoofClosed = inputs is { RoofOpened: false, RoofClosed: true };

        // Peek the top command and see if it is "Created".
        // This allows for exactly one command to execute at a time.
        if (_observatory.Commands.TryPeek(out var packedCommand)
            && packedCommand.Item1.State == CommandState.Created)
        {
            // Execute the command on a background thread so the loop continues.
            Task.Run(async () =>
            {
                var (command, commandToken) = packedCommand;
                _logger.LogInformation("Running {Command}", command);
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, commandToken);

                commandToken.Register(() => _logger.LogInformation("Command canceled"));

                try
                {
                    // Run the command
                    await command.Run(_observatory, linkedCts.Token);
                    _logger.LogInformation("Command complete");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Command exited via exception");
                }
                finally
                {
                    // Remove the command from the queue
                    while (!_observatory.Commands.TryDequeue(out var _))
                    {
                    }
                }
            }, token);
        }
    }
}
