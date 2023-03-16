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
    
    public bool RoofOpen { get; set; }
    public bool RoofClosed { get; set; }
    public bool CloudWatcherSafe { get; set; }
    public bool Tilt1 { get; set; }
    public bool Tilt1Inverted { get; set; }
    public bool Tilt2 { get; set; }
    public bool Tilt2Inverted { get; set; }
    public bool Tilt3 { get; set; }
    public bool Tilt3Inverted { get; set; }
    public bool Tilt4 { get; set; }
    public bool Tilt4Inverted { get; set; }
    public bool Input11 { get; set; }
    public bool Input12 { get; set; }
    public bool Input13 { get; set; }
    public bool Input14 { get; set; }
    public bool Input15 { get; set; }
}