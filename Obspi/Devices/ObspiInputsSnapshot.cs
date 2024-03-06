
using System.Reflection;

namespace Obspi.Devices;

public class ObspiInputsSnapshot : IObspiInputs
{
    private readonly Dictionary<string, PropertyInfo?> _thisProps;

    public ObspiInputsSnapshot(IObspiInputs inputs)
    {
        var otherProps = inputs
            .GetType()
            .GetProperties()
            .Where(p => p.PropertyType == typeof(bool));

        _thisProps = GetType()
            .GetProperties()
            .Where(p => p.PropertyType == typeof(bool))
            .ToDictionary(x => x.Name, x => x)!;

        foreach (var otherProp in otherProps)
        {
            _thisProps[otherProp.Name]!.SetValue(this, otherProp.GetValue(inputs));
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

    public IObspiInputs ToSnapshot() => this;

    public bool RoofOpened { get; set; }
    public bool RoofClosed { get; set; }
    public bool CloudWatcherUnsafe { get; set; }
    public bool TiltJoshSafe { get; set; }
    public bool TiltAlexSafe { get; set; }
    public bool Input6 { get; set; }
    public bool Input7 { get; set; }
    public bool Input8 { get; set; }
    public bool Input9 { get; set; }
    public bool Input10 { get; set; }
    public bool Input11 { get; set; }
    public bool Input12 { get; set; }
    public bool Input13 { get; set; }
    public bool Input14 { get; set; }
    public bool Input15 { get; set; }
    public bool Input16 { get; set; }
}