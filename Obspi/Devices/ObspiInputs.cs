namespace Obspi.Devices;

public class ObspiInputs : IObspiInputs
{
    private readonly IList<InputBank16> _banks;

    public ObspiInputs(IList<InputBank16> banks)
    {
        _banks = banks;
        Names = typeof(IObspiInputs)
            .GetProperties()
            .Where(p => p.PropertyType == typeof(bool))
            .Select(p => p.Name)
            .ToList();
    }

    public bool? GetValueOrNull(string name)
    {
        var prop = GetType().GetProperty(name);
        return prop?.GetValue(this) as bool?;
    }

    public IObspiInputs ToSnapshot() => new ObspiInputsSnapshot(this);

    public List<string> Names { get; }

    public bool RoofOpen
    {
        get => _banks[0][0];
        set => _banks[0][0] = value;
    }
    
    public bool RoofClosed
    {
        get => _banks[0][1];
        set => _banks[0][1] = value;
    }
    
    public bool CloudWatcherSafe
    {
        get => _banks[0][2];
        set => _banks[0][2] = value;
    }
    
    public bool Tilt1
    {
        get => _banks[0][3];
        set => _banks[0][3] = value;
    }
    
    public bool Tilt1Inverted
    {
        get => _banks[0][4];
        set => _banks[0][4] = value;
    }
    
    public bool Tilt2
    {
        get => _banks[0][5];
        set => _banks[0][5] = value;
    }
    
    public bool Tilt2Inverted
    {
        get => _banks[0][6];
        set => _banks[0][6] = value;
    }
    
    public bool Tilt3
    {
        get => _banks[0][7];
        set => _banks[0][7] = value;
    }
    
    public bool Tilt3Inverted
    {
        get => _banks[0][8];
        set => _banks[0][8] = value;
    }
    
    public bool Tilt4
    {
        get => _banks[0][9];
        set => _banks[0][9] = value;
    }
    
    public bool Tilt4Inverted
    {
        get => _banks[0][10];
        set => _banks[0][10] = value;
    }
    
    public bool Input11
    {
        get => _banks[0][11];
        set => _banks[0][11] = value;
    }
    
    public bool Input12
    {
        get => _banks[0][12];
        set => _banks[0][12] = value;
    }
    
    public bool Input13
    {
        get => _banks[0][13];
        set => _banks[0][13] = value;
    }
    
    public bool Input14
    {
        get => _banks[0][14];
        set => _banks[0][14] = value;
    }
    
    public bool Input15
    {
        get => _banks[0][15];
        set => _banks[0][15] = value;
    }
}