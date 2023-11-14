using Obspi.Services;

namespace Obspi.Commands;

public abstract class OpenCloseRoofCommand : Command
{
    private readonly INotificationService _notificationService;

    public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(10);

    public Action OnTimeout { get; init; } = () => { };

    public abstract Action<Observatory, bool> SetOutput { get; }

    public abstract Func<Observatory, bool> GetInput { get; }

    public abstract string Description { get; }

    public abstract string Verb { get; }

    public abstract string SuccessMessage { get; }

    public abstract string FailureMessage { get; }

    public abstract string TimeoutMessage { get; }

    public OpenCloseRoofCommand(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    protected override async Task Execute(Observatory observatory, CancellationToken token)
    {
        if (!observatory.IsRoofSafeToMove)
            throw new InvalidOperationException("Not safe to move roof");

        // Exit early if the roof is already in the desired state.
        if (GetInput(observatory))
            return;

        using var timeoutCts = new CancellationTokenSource(Timeout);
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(token, timeoutCts.Token);
        bool success = false;

        try
        {
            SetOutput(observatory, true);

            do
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100), cts.Token);

                // Check for roof safety at all times
                if (!observatory.IsRoofSafeToMove)
                {
                    await _notificationService.SendMessageAsync(
                        "Roof Became Unsafe",
                        "Roof became unsafe during motion.",
                        MessagePriority.Normal);
                    throw new InvalidOperationException("Roof became unsafe during move");
                }

                // Exit once the limit switch is made
                if (GetInput(observatory))
                {
                    success = true;
                    break;
                }
            } while (!cts.Token.IsCancellationRequested);
        }
        catch (OperationCanceledException) when (!timeoutCts.IsCancellationRequested)
        {
            await _notificationService.SendMessageAsync(
                $"Roof Stopped",
                $"Roof was manually stopped while {Verb.ToLower()}",
                MessagePriority.Normal);
        }
        finally
        {
            // Always turn off the output
            SetOutput(observatory, false);
        }

        if (timeoutCts.IsCancellationRequested)
        {
            await _notificationService.SendMessageAsync("Roof Timed Out", TimeoutMessage, MessagePriority.Low);
            OnTimeout?.Invoke();
        }
        
        if (success)
        {
            await _notificationService.SendMessageAsync($"Roof {Description}", SuccessMessage, MessagePriority.Normal);
        }
        else
        {
            await _notificationService.SendMessageAsync($"Roof Failed To {Description}", FailureMessage, MessagePriority.Normal);
        }
    }
}

public class OpenRoofCommand : OpenCloseRoofCommand
{
    public override Action<Observatory, bool> SetOutput { get; } = (obs, value) => obs.IO.Outputs.RoofOpen = value;
    public override Func<Observatory, bool> GetInput { get; } = obs => obs.IO.Inputs.RoofOpened;
    public override string Description => "Open";
    public override string Verb => "Opening";
    public override string SuccessMessage => "Roof is now open.";
    public override string FailureMessage => "Roof failed to open.";
    public override string TimeoutMessage => "Roof timed out while opening.";

    public OpenRoofCommand(INotificationService notificationService)
        : base(notificationService)
    {
    }
}

public class CloseRoofCommand : OpenCloseRoofCommand
{
    public override Action<Observatory, bool> SetOutput { get; } = (obs, value) => obs.IO.Outputs.RoofClose = value;
    public override Func<Observatory, bool> GetInput { get; } = obs => obs.IO.Inputs.RoofClosed;
    public override string Description => "Closed";
    public override string Verb => "Closing";
    public override string SuccessMessage => "Roof is now closed.";
    public override string FailureMessage => "Roof failed to close.";
    public override string TimeoutMessage => "Roof timed out while closing.";

    public CloseRoofCommand(INotificationService notificationService)
        : base(notificationService)
    {
    }
}