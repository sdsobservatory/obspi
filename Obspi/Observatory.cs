using System.Collections.Concurrent;
using Obspi.Devices;
using Obspi.Commands;

namespace Obspi;

public interface IObservatory
{
    ICloudWatcher CloudWatcher { get; }
    ConcurrentQueue<(Command, CancellationToken)> Commands { get; }
    IIndustrialAutomation IndustrialAutomation { get; }
    IObspiIO IO { get; }
    bool IsRoofClosed { get; set; }
    bool IsRoofOpen { get; set; }
    bool IsRoofSafeToMove { get; set; }
    TimeSpan LoopTime { get; set; }
    CancellationTokenSource? RoofCts { get; set; }
    ISqmLe Sqm { get; }

    void EnqueueCommand(Command command, CancellationToken token = default);
}

public class Observatory : IObservatory
{
    public Observatory(
        IIndustrialAutomation industrialAutomation,
        IObspiIO io,
        ISqmLe sqm,
        ICloudWatcher cloudWatcher)
    {
        IO = io;
        IndustrialAutomation = industrialAutomation;
        Sqm = sqm;
        CloudWatcher = cloudWatcher;
    }

    public void EnqueueCommand(Command command, CancellationToken token = default)
    {
        Commands.Enqueue((command, token));
    }

    public IObspiIO IO { get; }
    public IIndustrialAutomation IndustrialAutomation { get; }
    public ISqmLe Sqm { get; }
    public ICloudWatcher CloudWatcher { get; }

    public ConcurrentQueue<(Command, CancellationToken)> Commands { get; } = new();

    public bool IsRoofSafeToMove { get; set; }

    public bool IsRoofOpen { get; set; }

    public bool IsRoofClosed { get; set; }

    public TimeSpan LoopTime { get; set; }

    public CancellationTokenSource? RoofCts { get; set; }
}