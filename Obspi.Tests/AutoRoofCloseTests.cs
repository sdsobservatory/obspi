using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Obspi.Commands;
using Obspi.Services;

namespace Obspi.Tests;

public class AutoRoofCloseTests : IAsyncDisposable
{
    private const int PhoenixUtcOffset = -7;

    private readonly ServiceProvider _serviceProvider;
    private readonly AsyncServiceScope _scope;

    private readonly FakeTimeProvider _timeProvider;
    private readonly IObservatory _observatory = Mock.Of<IObservatory>();
    private readonly INotificationService _notifications = Mock.Of<INotificationService>();


    public AutoRoofCloseTests()
    {
        // Create the time provider and set it to midnight phoenix time
        _timeProvider = new FakeTimeProvider();
        _timeProvider.SetLocalTimeZone(TZConvert.GetTimeZoneInfo("America/Phoenix"));

        // Mock EnqueueCommand to fake the cmd execution
        Mock.Get(_observatory)
            .Setup(x => x.EnqueueCommand(It.IsAny<Command>(), It.IsAny<CancellationToken>()))
            .Callback<Command, CancellationToken>((cmd, _) => cmd.SetComplete());

        var services = new ServiceCollection();
        services.AddSingleton<TimeProvider>(_timeProvider);
        services.AddTransient<ILogger<AutoRoofCloseHostedService>>(_ => Mock.Of<ILogger<AutoRoofCloseHostedService>>());
        services.AddScoped<AutoRoofCloseHostedService>();
        services.AddScoped<IObservatory>(_ => _observatory);
        services.AddScoped<INotificationService>(_ => _notifications);
        _serviceProvider = services.BuildServiceProvider();
        _scope = _serviceProvider.CreateAsyncScope();
    }

    public async ValueTask DisposeAsync()
    {
        await _scope.DisposeAsync();
        await _serviceProvider.DisposeAsync();
    }

    private void SetTimeToSunriseWithOffset(int minuteOffset)
    {
        var currentMonth = 3;
        var currentTime = AutoRoofCloseHostedService.SunRiseTable[currentMonth].AddMinutes(minuteOffset);
        var utcNow = new DateTimeOffset(new DateOnly(2024, currentMonth, 2), currentTime.AddHours(-PhoenixUtcOffset), TimeSpan.Zero);
        _timeProvider.SetUtcNow(utcNow);
    }

    [Fact]
    public async Task RoofAlreadyClosed_DoesNothing()
    {
        Mock.Get(_observatory).Setup(x => x.IsRoofClosed).Returns(true);

        var service = _scope.ServiceProvider.GetRequiredService<AutoRoofCloseHostedService>();
        await service.OnExecuteTest(_scope, CancellationToken.None);

        Mock.Get(_notifications)
            .Verify(x => x.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessagePriority>()), Times.Never);
        Mock.Get(_observatory)
            .Verify(x => x.EnqueueCommand(It.IsAny<Command>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task RoofOpen_BeforeSunrise_DoesNothing()
    {
        SetTimeToSunriseWithOffset(-1);

        var service = _scope.ServiceProvider.GetRequiredService<AutoRoofCloseHostedService>();
        await service.OnExecuteTest(_scope, CancellationToken.None);

        Mock.Get(_notifications)
            .Verify(x => x.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessagePriority>()), Times.Never);
        Mock.Get(_observatory)
            .Verify(x => x.EnqueueCommand(It.IsAny<Command>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task RoofOpen_AfterSunrise_ClosesRoof()
    {
        Mock.Get(_observatory).Setup(x => x.IsRoofSafeToMove).Returns(true);

        SetTimeToSunriseWithOffset(1);

        var service = _scope.ServiceProvider.GetRequiredService<AutoRoofCloseHostedService>();
        await service.OnExecuteTest(_scope, CancellationToken.None);

        Mock.Get(_notifications)
            .Verify(x => x.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessagePriority>()), Times.Never);
        Mock.Get(_observatory)
            .Verify(x => x.EnqueueCommand(It.IsAny<CloseRoofCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RoofOpen_AfterSunrise_MultipleAttempts_SendNormalNotification()
    {
        var service = _scope.ServiceProvider.GetRequiredService<AutoRoofCloseHostedService>();
        SetTimeToSunriseWithOffset(-1);

        for (int i = 0; i < 41; i++)
        {
            await service.OnExecuteTest(_scope, CancellationToken.None);
            _timeProvider.Advance(TimeSpan.FromMinutes(1));
        }

        Mock.Get(_notifications)
            .Verify(x => x.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>(), MessagePriority.Normal), Times.Exactly(2));
    }

    [Fact]
    public async Task RoofOpen_AfterSunrise_MultipleAttempts_SendNormalAndEmergencyNotification()
    {
        var service = _scope.ServiceProvider.GetRequiredService<AutoRoofCloseHostedService>();
        SetTimeToSunriseWithOffset(-1);

        for (int i = 0; i < 103; i++)
        {
            await service.OnExecuteTest(_scope, CancellationToken.None);
            _timeProvider.Advance(TimeSpan.FromMinutes(1));
        }

        Mock.Get(_notifications)
            .Verify(x => x.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>(), MessagePriority.Normal), Times.Exactly(12));
        Mock.Get(_notifications)
            .Verify(x => x.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>(), MessagePriority.Emergency), Times.Exactly(3));
    }

    [Fact]
    public async Task RoofOpen_AfterSunrise_MultipleAttempts_SendNormalAndEmergencyNotification_RoofEventuallyIsClosed()
    {
        var service = _scope.ServiceProvider.GetRequiredService<AutoRoofCloseHostedService>();
        SetTimeToSunriseWithOffset(-1);

        for (int i = 0; i < 103; i++)
        {
            await service.OnExecuteTest(_scope, CancellationToken.None);
            _timeProvider.Advance(TimeSpan.FromMinutes(1));
        }

        Mock.Get(_notifications)
            .Verify(x => x.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>(), MessagePriority.Normal), Times.Exactly(12));
        Mock.Get(_notifications)
            .Verify(x => x.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>(), MessagePriority.Emergency), Times.Exactly(3));
        Mock.Get(_observatory)
            .Verify(x => x.EnqueueCommand(It.IsAny<CloseRoofCommand>(), It.IsAny<CancellationToken>()), Times.Never);

        Mock.Get(_notifications).Reset();
        Mock.Get(_observatory).Setup(x => x.IsRoofSafeToMove).Returns(true);

        await service.OnExecuteTest(_scope, CancellationToken.None);

        Mock.Get(_notifications)
            .Verify(x => x.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>(), MessagePriority.Normal), Times.Never);
        Mock.Get(_notifications)
            .Verify(x => x.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>(), MessagePriority.Emergency), Times.Never);
        Mock.Get(_observatory)
            .Verify(x => x.EnqueueCommand(It.IsAny<CloseRoofCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}