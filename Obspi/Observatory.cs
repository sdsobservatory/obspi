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

    public bool CanRoofOpen { get; set; }

    public bool CanRoofClose { get; set; }

    public bool IsRoofOpen { get; set; }
    
    public bool IsRoofClosed { get; set; }
    
    public TimeSpan LoopTime { get; set; }
}