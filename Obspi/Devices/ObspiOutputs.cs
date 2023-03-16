namespace Obspi.Devices;

public class ObspiOutputs : IObspiOutputs
{
    private readonly IList<OutputBank16> _banks;

    public ObspiOutputs(IList<OutputBank16> banks)
    {
        _banks = banks;
        Names = typeof(IObspiOutputs)
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

    public bool TrySetValue(string name, bool state)
    {
        var prop = GetType().GetProperty(name);
        if (prop is not { })
            return false;
        
        prop.SetValue(this, state);
        return true;
    }

    public IObspiOutputs ToSnapshot() => new ObspiOutputsSnapshot(this);
    
    public List<string> Names { get; }

    public bool RoofMotor
    {
        get => _banks[0][0];
        set => _banks[0][0] = value;
    }
    
    public bool Pier1AC
    {
        get => _banks[0][1];
        set => _banks[0][1] = value;
    }
    
    public bool Pier2AC
    {
        get => _banks[0][2];
        set => _banks[0][2] = value;
    }
    
    public bool Pier3AC
    {
        get => _banks[0][3];
        set => _banks[0][3] = value;
    }
    
    public bool Pier4AC
    {
        get => _banks[0][4];
        set => _banks[0][4] = value;
    }
    
    public bool Pier1DC
    {
        get => _banks[0][5];
        set => _banks[0][5] = value;
    }
    
    public bool Pier2DC
    {
        get => _banks[0][6];
        set => _banks[0][6] = value;
    }
    
    public bool Pier3DC
    {
        get => _banks[0][7];
        set => _banks[0][7] = value;
    }
    
    public bool Pier4DC
    {
        get => _banks[0][8];
        set => _banks[0][8] = value;
    }

    public bool TenMicron1
    {
        get => _banks[0][9];
        set => _banks[0][9] = value;
    }
    
    public bool TenMicron2
    {
        get => _banks[0][10];
        set => _banks[0][10] = value;
    }
    
    public bool Bank0Channel11
    {
        get => _banks[0][11];
        set => _banks[0][11] = value;
    }
    
    public bool Bank0Channel12
    {
        get => _banks[0][12];
        set => _banks[0][12] = value;
    }
    
    public bool Bank0Channel13
    {
        get => _banks[0][13];
        set => _banks[0][13] = value;
    }
    
    public bool Bank0Channel14
    {
        get => _banks[0][14];
        set => _banks[0][14] = value;
    }
    
    public bool Bank0Channel15
    {
        get => _banks[0][15];
        set => _banks[0][15] = value;
    }
    
    public bool Bank1Channel0
    {
        get => _banks[1][0];
        set => _banks[1][0] = value;
    }
    
    public bool Bank1Channel1
    {
        get => _banks[1][1];
        set => _banks[1][1] = value;
    }
    
    public bool Bank1Channel2
    {
        get => _banks[1][2];
        set => _banks[1][2] = value;
    }
    
    public bool Bank1Channel3
    {
        get => _banks[1][3];
        set => _banks[1][3] = value;
    }
    
    public bool Bank1Channel4
    {
        get => _banks[1][4];
        set => _banks[1][4] = value;
    }
    
    public bool Bank1Channel5
    {
        get => _banks[1][5];
        set => _banks[1][5] = value;
    }
    
    public bool Bank1Channel6
    {
        get => _banks[1][6];
        set => _banks[1][6] = value;
    }
    
    public bool Bank1Channel7
    {
        get => _banks[1][7];
        set => _banks[1][7] = value;
    }
    
    public bool Bank1Channel8
    {
        get => _banks[1][8];
        set => _banks[1][8] = value;
    }
    
    public bool Bank1Channel9
    {
        get => _banks[1][9];
        set => _banks[1][9] = value;
    }
    
    public bool Bank1Channel10
    {
        get => _banks[1][10];
        set => _banks[1][10] = value;
    }
    
    public bool Bank1Channel11
    {
        get => _banks[1][11];
        set => _banks[1][11] = value;
    }
    
    public bool Bank1Channel12
    {
        get => _banks[1][12];
        set => _banks[1][12] = value;
    }
    
    public bool Bank1Channel13
    {
        get => _banks[1][13];
        set => _banks[1][13] = value;
    }
    
    public bool Bank1Channel14
    {
        get => _banks[1][14];
        set => _banks[1][14] = value;
    }
    
    public bool Bank1Channel15
    {
        get => _banks[1][15];
        set => _banks[1][15] = value;
    }
}