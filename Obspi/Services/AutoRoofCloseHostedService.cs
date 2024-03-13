using Obspi.Commands;

namespace Obspi.Services;

public class AutoRoofCloseHostedService : PeriodicHostedService
{
    private readonly ILogger<AutoRoofCloseHostedService> _logger;
    private readonly TimeProvider _timeProvider;

    private const int MinutesPastSunriseNormalPriorityAlert = 30;
    private const int MinutesPastSunriseEmergencyPriorityAlert = 90;
    private const int AlertInterval = 5;  // every 5 minutes

    /// <summary>
    /// The time when the sun is approx. 3 deg above horizon.
    /// </summary>
    internal static readonly Dictionary<int, TimeOnly> SunRiseTable = new()
    {
#pragma warning disable format
        [1]  = new(7, 45),
        [2]  = new(7, 37),
        [3]  = new(7, 09),
        [4]  = new(6, 29),
        [5]  = new(5, 56),
        [6]  = new(5, 38),
        [7]  = new(5, 41),
        [8]  = new(5, 58),
        [9]  = new(6, 18),
        [10] = new(6, 36),
        [11] = new(7, 01),
        [12] = new(7, 28),
#pragma warning restore format
    };

    public AutoRoofCloseHostedService(
        ILogger<AutoRoofCloseHostedService> logger,
        IServiceScopeFactory factory,
        TimeProvider timeProvider)
        : base(logger, factory)
    {
        _logger = logger;
        _timeProvider = timeProvider;

        // 1 minute if plenty fast enough.
        Period = TimeSpan.FromMinutes(1);
    }

    protected internal Task OnExecuteTest(AsyncServiceScope scope, CancellationToken stoppingToken) => OnExecute(scope, stoppingToken);

    protected override async Task OnExecute(AsyncServiceScope scope, CancellationToken stoppingToken)
    {
        var observatory = scope.ServiceProvider.GetRequiredService<IObservatory>();
        var notification = scope.ServiceProvider.GetRequiredService<INotificationService>();

        _logger.LogInformation("Current Time: {CurrentTime}, Sunrise Time: {SunriseTime}, Normal Alert Time: {NormalAlertTime}, Critical Alert Time: {CriticalAlertTime}",
            CurrentTime, Sunrise, NormalAlertTime, EmergencyAlertTime);

        // Ignore if the roof is already closed
        if (observatory.IsRoofClosed)
        {
            _logger.LogInformation("Roof already closed");
            return;
        }

        // Is it time to start caring about closing the roof?
        if (CurrentTime < new TimeOnly(12, 00) && CurrentTime >= Sunrise)
        {
            _logger.LogInformation("The sun is coming up, time to close the roof");

            if (CurrentTime >= EmergencyAlertTime &&
                CurrentTime.Minute % AlertInterval == 0)
            {
                _logger.LogInformation($"The roof has not been closed, sending emergency priority alerts");
                await notification.SendMessageAsync(
                    title: "Auto Roof Close Failed",
                    message: "Roof did not auto close, check roof now!",
                    priority: MessagePriority.Emergency);

                // Do not attempt to close the roof since all previous attempts failed.
                return;
            }
            else if (CurrentTime >= NormalAlertTime &&
                CurrentTime.Minute % AlertInterval == 0)
            {
                _logger.LogInformation($"The roof has not been closed, sending normal priority alerts");
                await notification.SendMessageAsync(
                    title: "Auto Roof Close Failed",
                    message: "Roof did not auto close, check roof now!",
                    priority: MessagePriority.Normal);

                // Do not attempt to close the roof since all previous attempts failed.
                return;
            }

            if (!observatory.IsRoofSafeToMove)
            {
                _logger.LogInformation($"The roof is not safe to move, trying again in {Period.TotalMinutes:F0} minutes");
                return;
            }

            _logger.LogInformation("Closing the roof");

            var cmd = new CloseRoofCommand(notification) { Timeout = TimeSpan.FromSeconds(60) };
            observatory.EnqueueCommand(cmd, stoppingToken);
            await cmd.WaitAsync(stoppingToken);

            if (observatory.IsRoofClosed)
            {
                _logger.LogInformation("Roof closed");
            }
            else
            {
                _logger.LogInformation($"The roof did not close, trying again in {Period.TotalMinutes:F0} minutes");
            }
        }
        else
        {
            _logger.LogInformation("Not yet time to close the roof");
        }
    }

    public DateTime Now => _timeProvider.GetLocalNow().LocalDateTime;

    public TimeOnly CurrentTime => TimeOnly.FromDateTime(Now);

    public TimeOnly Sunrise => SunRiseTable[Now.Month];

    public TimeOnly NormalAlertTime => Sunrise.AddMinutes(MinutesPastSunriseNormalPriorityAlert);

    public TimeOnly EmergencyAlertTime => Sunrise.AddMinutes(MinutesPastSunriseEmergencyPriorityAlert);
}
