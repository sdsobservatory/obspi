using System.Linq.Expressions;
using Obspi.Devices;

namespace Obspi.Commands;

public class PulsedIoCommand : Command
{
    public TimeSpan Delay { get; }
    public bool Value { get; }
    public Expression<Func<IObspiOutputs, bool>> Selector { get; }

    public PulsedIoCommand(TimeSpan delay, bool value, Expression<Func<IObspiOutputs, bool>> selector)
    {
        Delay = delay;
        Value = value;
        Selector = selector;
    }

    protected override async Task Execute(Observatory observatory, CancellationToken token)
    {
        var prop = Selector.GetPropertyInfo();
        prop.SetValue(observatory.IO.Outputs, Value);
        await Task.Delay(Delay, token);
        prop.SetValue(observatory.IO.Outputs, !Value);
    }

    public override string ToString()
    {
        var prop = Selector.GetPropertyInfo();
        return $"{nameof(PulsedIoCommand)} (Delay={Delay.TotalSeconds:F3},Value={Value},Output={prop.Name})";
    }
}