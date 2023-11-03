using System.Collections.Concurrent;
using Obspi.Devices;
using Obspi.Commands;

namespace Obspi;

public class Observatory
{
    public Observatory(
        IndustrialAutomation industrialAutomation,
        ObspiIO io,
        SqmLe sqm,
        CloudWatcher cloudWatcher)
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
    
    public ObspiIO IO { get; }
    public IndustrialAutomation IndustrialAutomation { get; }
    public SqmLe Sqm { get; }
    public CloudWatcher CloudWatcher { get; }

    public ConcurrentQueue<(Command, CancellationToken)> Commands { get; } = new();

    public bool IsRoofSafeToMove { get; set; }

    public bool IsRoofOpen { get; set; }
    
    public bool IsRoofClosed { get; set; }
    
    public TimeSpan LoopTime { get; set; }

    public CancellationTokenSource? RoofCts { get; set; }
}