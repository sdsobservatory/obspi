using System.Buffers.Binary;
using Obspi.Devices.I2c;

namespace Obspi.Devices;

public abstract class IoBank16 : IIoBank
{
    private readonly II2cDevice _i2c;

    protected enum Register : byte
    {
        InputPort = 0,
        OutputPort = 2,
        Polarity = 4,
        Config = 6
    }
    
    private static readonly Dictionary<int, int> PortMap = new()
    {
        [0] = 1 << 7,
        [1] = 1 << 6,
        [2] = 1 << 5,
        [3] = 1 << 4,
        [4] = 1 << 3,
        [5] = 1 << 2,
        [6] = 1 << 1,
        [7] = 1 << 0,
        [8] = 1 << 15,
        [9] = 1 << 14,
        [10] = 1 << 13,
        [11] = 1 << 12,
        [12] = 1 << 11,
        [13] = 1 << 10,
        [14] = 1 << 9,
        [15] = 1 << 8,
    };

    public int Count => 16;
    
    public int Value
    {
        get => GetValue();
        set => SetValue(value);
    }

    public bool this[int i]
    {
        get => GetValueAt(i);
        set => SetValueAt(i, value);
    }

    public IoBank16(II2cDevice i2c)
    {
        _i2c = i2c;
    }

    protected abstract byte GetPortRegister();
    
    public int GetValue()
    {
        Span<byte> writeBuffer = stackalloc byte[1] { GetPortRegister() };
        Span<byte> readBuffer = stackalloc byte[2];
        _i2c.WriteRead(writeBuffer, readBuffer);

        var raw = BinaryPrimitives.ReadUInt16BigEndian(readBuffer);
        var value = 0;
        foreach (var (index, mask) in PortMap)
        {
            if ((raw & (1 << index)) != 0)
                value |= mask;
        }

        return value;
    }

    public void SetValue(int value)
    {
        int tmp = 0;
        foreach (var (index, mask) in PortMap)
        {
            if ((value & (1 << index)) != 0)
                tmp |= mask;
        }
        
        Span<byte> buffer = stackalloc byte[3];
        buffer[0] = GetPortRegister();
        BinaryPrimitives.WriteUInt16BigEndian(buffer.Slice(1, 2), (ushort) tmp);
        _i2c.Write(buffer);
    }

    public bool GetValueAt(int index)
    {
        if (index < 0 || index >= Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        var value = GetValue();
        return (value & (1 << index)) != 0;
    }
    
    public void SetValueAt(int index, bool value)
    {
        if (index < 0 || index >= Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        var current = GetValue();
        
        if (value)
        {
            current |= 1 << index;
        }
        else
        {
            current &= ~(1 << index);
        }
        
        SetValue(current);
    }
}