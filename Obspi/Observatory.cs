using System.Collections.Concurrent;
using Obspi.Commands;
using Obspi.Devices;

namespace Obspi;

public class Observatory
{
    public Observatory(
        IndustrialAutomation industrialAutomation,
        ObspiIO io)
    {
        IO = io;
        IndustrialAutomation = industrialAutomation;
    }

    public void EnqueueCommand(Command command)
    {
        Commands.Enqueue(command);
    }
    
    public ObspiIO IO { get; }
    public IndustrialAutomation IndustrialAutomation { get; }
    public ConcurrentQueue<Command> Commands { get; } = new();

    /// <summary>
    /// When true, it is safe to open or close the roof.
    /// </summary>
    public bool CanRoofMove { get; set; }

    public bool IsRoofOpen { get; set; }
    
    public bool IsRoofClosed { get; set; }
    
    public TimeSpan LoopTime { get; set; }
}