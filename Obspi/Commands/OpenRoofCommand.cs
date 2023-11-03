namespace Obspi.Commands;

public abstract class OpenCloseRoofCommand : Command
{
    public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(10);

    public Action OnTimeout { get; init; } = () => { };

    public abstract Action<Observatory, bool> SetOutput { get; }

    public abstract Func<Observatory, bool> GetInput { get; }

    protected override async Task Execute(Observatory observatory, CancellationToken token)
    {
        if (!observatory.IsRoofSafeToMove)
            throw new InvalidOperationException("Not safe to move roof");

        // Exit early if the roof is already in the desired state.
        if (GetInput(observatory))
            return;

        using var timeoutCts = new CancellationTokenSource(Timeout);
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(token, timeoutCts.Token);

        try
        {
            SetOutput(observatory, true);

            do
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100), cts.Token);
                
                // Check for roof safety at all times
                if (!observatory.IsRoofSafeToMove)
                    throw new InvalidOperationException("Roof became unsafe during move");

                // Exit once the limit switch is made
                if (GetInput(observatory))
                {
                    break;
                }
            } while (!cts.Token.IsCancellationRequested);
        }
        finally
        {
            // Always turn off the output
            SetOutput(observatory, false);
        }

        if (timeoutCts.IsCancellationRequested)
        {
            OnTimeout?.Invoke();
        }
    }
}

public class OpenRoofCommand : OpenCloseRoofCommand
{
    public override Action<Observatory, bool> SetOutput { get; } = (obs, value) => obs.IO.Outputs.RoofOpen = value;
    public override Func<Observatory, bool> GetInput { get; } = obs => obs.IO.Inputs.RoofOpened;
}

public class CloseRoofCommand : OpenCloseRoofCommand
{
    public override Action<Observatory, bool> SetOutput { get; } = (obs, value) => obs.IO.Outputs.RoofClose = value;
    public override Func<Observatory, bool> GetInput { get; } = obs => obs.IO.Inputs.RoofClosed;
}