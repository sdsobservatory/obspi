namespace Obspi.Devices;

public class ObspiOutputsSnapshot : IObspiOutputs
{
    public ObspiOutputsSnapshot(IObspiOutputs outputs)
    {
        var otherProps = outputs
            .GetType()
            .GetProperties()
            .Where(p => p.PropertyType == typeof(bool));
        
        var thisProps = GetType()
            .GetProperties()
            .Where(p => p.PropertyType == typeof(bool))
            .ToDictionary(x => x.Name, x => x);

        foreach (var otherProp in otherProps)
        {
            thisProps[otherProp.Name].SetValue(this, otherProp.GetValue(outputs));
        }
    }
    
    public bool RoofMotor { get; set; }
    public bool Pier1AC { get; set; }
    public bool Pier2AC { get; set; }
    public bool Pier3AC { get; set; }
    public bool Pier4AC { get; set; }
    public bool Pier1DC { get; set; }
    public bool Pier2DC { get; set; }
    public bool Pier3DC { get; set; }
    public bool Pier4DC { get; set; }
    public bool TenMicron1 { get; set; }
    public bool TenMicron2 { get; set; }
    public bool Bank0Channel11 { get; set; }
    public bool Bank0Channel12 { get; set; }
    public bool Bank0Channel13 { get; set; }
    public bool Bank0Channel14 { get; set; }
    public bool Bank0Channel15 { get; set; }
    public bool Bank1Channel0 { get; set; }
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
}