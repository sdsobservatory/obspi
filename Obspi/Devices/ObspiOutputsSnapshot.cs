using System.Reflection;

namespace Obspi.Devices;

public class ObspiOutputsSnapshot : IObspiOutputs
{
    private readonly Dictionary<string, PropertyInfo?> _thisProps;

    public ObspiOutputsSnapshot(IObspiOutputs outputs)
    {
        var otherProps = outputs
            .GetType()
            .GetProperties()
            .Where(p => p.PropertyType == typeof(bool));

        _thisProps = GetType()
            .GetProperties()
            .Where(p => p.PropertyType == typeof(bool))
            .ToDictionary(x => x.Name, x => x)!;

        foreach (var otherProp in otherProps)
        {
            _thisProps[otherProp.Name]!.SetValue(this, otherProp.GetValue(outputs));
        }
    }

    public List<string> Names => throw new NotImplementedException();

    public bool? GetValueOrNull(string name)
    {
        if (_thisProps.TryGetValue(name, out var value))
        {
            return value!.GetValue(this) as bool?;
        }

        return null;
    }

    public bool TrySetValue(string name, bool state)
    {
        throw new NotImplementedException();
    }

    public IObspiOutputs ToSnapshot() => this;

    public bool Suicide { get; set; }
    public bool RoofOpen { get; set; }
    public bool RoofClose { get; set; }
    public bool Josh1ACReset { get; set; }
    public bool Josh1DCReset { get; set; }
    public bool Josh2ACReset { get; set; }
    public bool Josh2DCReset { get; set; }
    public bool AlexACReset { get; set; }
    public bool AlexDCReset { get; set; }
    public bool CharlieACReset { get; set; }
    public bool CharlieDCReset { get; set; }
    public bool Josh10MicronReset { get; set; }
    public bool Alex10MicronReset { get; set; }
    public bool Bank0Channel14 { get; set; }
    public bool Bank0Channel15 { get; set; }
    public bool Bank0Channel16 { get; set; }
    public bool Bank1Channel1 { get; set; }
    public bool Bank1Channel2 { get; set; }
    public bool Bank1Channel3 { get; set; }
    public bool Bank1Channel4 { get; set; }
    public bool Bank1Channel5 { get; set; }
    public bool Bank1Channel6 { get; set; }
    public bool Bank1Channel7 { get; set; }
    public bool Bank1Channel8 { get; set; }
    public bool Bank1Channel9 { get; set; }
    public bool Bank1Channel10 { get; set; }
    public bool Bank1Channel11 { get; set; }
    public bool Bank1Channel12 { get; set; }
    public bool Bank1Channel13 { get; set; }
    public bool Bank1Channel14 { get; set; }
    public bool Bank1Channel15 { get; set; }
    public bool Bank1Channel16 { get; set; }
}