namespace Obspi.Devices;

public class ObspiInputsSnapshot : IObspiInputs
{
    public ObspiInputsSnapshot(IObspiInputs inputs)
    {
        var otherProps = inputs
            .GetType()
            .GetProperties()
            .Where(p => p.PropertyType == typeof(bool));
        
        var thisProps = GetType()
            .GetProperties()
            .Where(p => p.PropertyType == typeof(bool))
            .ToDictionary(x => x.Name, x => x);

        foreach (var otherProp in otherProps)
        {
            thisProps[otherProp.Name].SetValue(this, otherProp.GetValue(inputs));
        }
    }

    public bool RoofOpened { get; set; }
    public bool RoofClosed { get; set; }
    public bool CloudWatcherUnsafe { get; set; }
    public bool TiltJosh { get; set; }
    public bool TiltAlex { get; set; }
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