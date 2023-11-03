using Obspi.Devices.I2c;

namespace Obspi.Devices;

public class OutputBank16 : IoBank16
{
    public OutputBank16(II2cDevice i2c)
        : base(i2c)
    {
        Span<byte> buffer = stackalloc byte[3];

        // Normal polarity
        buffer[0] = (byte)Register.Polarity;
        buffer[1] = 0;
        buffer[2] = 0;
        i2c.Write(buffer);

        // Turn off all outputs
        buffer[0] = (byte)Register.OutputPort;
        buffer[1] = 0;
        buffer[2] = 0;
        i2c.Write(buffer);

        // Configure as output
        buffer[0] = (byte)Register.Config;
        buffer[1] = 0;
        buffer[2] = 0;
        i2c.Write(buffer);
    }

    protected override byte GetPortRegister() => (byte) Register.OutputPort;
}