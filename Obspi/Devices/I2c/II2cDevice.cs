using System.Device.I2c;

namespace Obspi.Devices.I2c;

public interface II2cDevice : IDisposable
{
    I2cConnectionSettings ConnectionSettings { get; }

    byte ReadByte();
    void Read(Span<byte> buffer);
    void WriteByte(byte value);
    void Write(ReadOnlySpan<byte> buffer);
    void WriteRead(ReadOnlySpan<byte> writeBuffer, Span<byte> readBuffer);
}