using Obspi.Devices.I2c;

namespace Obspi.Devices;

public class InputBank16 : IoBank16
{
    public InputBank16(II2cDevice i2c)
        : base(i2c)
    {
        Span<byte> buffer = stackalloc byte[3];

        // Disable outputs
        buffer[0] = (byte)Register.OutputPort;
        buffer[1] = 0;
        buffer[2] = 0;
        i2c.Write(buffer);

        // Configure as input
        buffer[0] = (byte) Register.Config;
        buffer[1] = 0xff;
        buffer[2] = 0xff;
        i2c.Write(buffer);

        // Inverse polarity
        buffer[0] = (byte) Register.Polarity;
        buffer[1] = 0xff;
        buffer[2] = 0xff;
        i2c.Write(buffer);
    }

    protected override byte GetPortRegister() => (byte) Register.InputPort;
}