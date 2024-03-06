using System.Buffers.Binary;
using Obspi.Devices.I2c;

namespace Obspi.Devices;

public interface IIndustrialAutomation
{
    double Get24VRailVoltage();
    double Get5VRailVoltage();
    double GetAnalogOut(IndustrialAutomation.AnalogChannel channel);
    int GetCpuTemperature();
    DateTime GetDateTime();
    Version GetFirmwareVersion();
    double GetRtcBatteryVoltage();
    void SetAnalogOut(IndustrialAutomation.AnalogChannel channel, double value);
    void SetDateTime(DateTime datetime);
}

public class IndustrialAutomation : IIndustrialAutomation
{
    private readonly II2cDevice _device;

    public static byte CalibrationKey => 0xaa;

    private enum Register : byte
    {
        AnalogOutChannel1 = 0x04,
        AnalogOutChannel2 = 0x06,
        AnalogOutChannel3 = 0x08,
        AnalogOutChannel4 = 0x0a,
        GetRtc = 0x46,
        SetRtc = 0x4c,
        DiagnosticsCpuTemperature = 0x72,
        Diagnostics24VRail = 0x73,
        Diagnostics5VRail = 0x75,
        FirmwareMajor = 0x78,
        FirmwareMinor = 0x79,
        BatteryVoltage = 0x91,
    }

    public enum AnalogChannel
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
    };

    public IndustrialAutomation(II2cDevice device)
    {
        _device = device;

        // Load fake in-memory values
        if (device is InMemoryI2cDevice inMemoryI2CDevice)
            LoadDefaultValues(inMemoryI2CDevice);
    }

    public static void LoadDefaultValues(InMemoryI2cDevice i2c)
    {
        Span<byte> buffer = stackalloc byte[2];

        i2c.Data[(int)Register.DiagnosticsCpuTemperature] = 25;
        i2c.Data[(int)Register.FirmwareMajor] = 1;
        i2c.Data[(int)Register.FirmwareMinor] = 12;

        var now = DateTime.UtcNow;
        i2c.Data[(int)Register.GetRtc] = (byte)(now.Year - 2000);
        i2c.Data[(int)Register.GetRtc + 1] = (byte)now.Month;
        i2c.Data[(int)Register.GetRtc + 2] = (byte)now.Day;
        i2c.Data[(int)Register.GetRtc + 3] = (byte)now.Hour;
        i2c.Data[(int)Register.GetRtc + 4] = (byte)now.Minute;
        i2c.Data[(int)Register.GetRtc + 5] = (byte)now.Second;

        BinaryPrimitives.WriteInt16LittleEndian(buffer, 24052);
        i2c.Data[(int)Register.Diagnostics24VRail] = buffer[0];
        i2c.Data[(int)Register.Diagnostics24VRail + 1] = buffer[1];

        BinaryPrimitives.WriteInt16LittleEndian(buffer, 5031);
        i2c.Data[(int)Register.Diagnostics5VRail] = buffer[0];
        i2c.Data[(int)Register.Diagnostics5VRail + 1] = buffer[1];

        BinaryPrimitives.WriteInt16LittleEndian(buffer, 3314);
        i2c.Data[(int)Register.BatteryVoltage] = buffer[0];
        i2c.Data[(int)Register.BatteryVoltage + 1] = buffer[1];
    }

    public int GetCpuTemperature()
    {
        Span<byte> readBuffer = stackalloc byte[1];
        Span<byte> writeBuffer = stackalloc byte[1];
        writeBuffer[0] = (byte)Register.DiagnosticsCpuTemperature;
        _device.WriteRead(writeBuffer, readBuffer);
        return readBuffer[0];
    }

    public double Get24VRailVoltage()
    {
        Span<byte> readBuffer = stackalloc byte[2];
        Span<byte> writeBuffer = stackalloc byte[1];
        writeBuffer[0] = (byte)Register.Diagnostics24VRail;
        _device.WriteRead(writeBuffer, readBuffer);
        return BinaryPrimitives.ReadInt16LittleEndian(readBuffer) / 1000.0;
    }

    public double Get5VRailVoltage()
    {
        Span<byte> readBuffer = stackalloc byte[2];
        Span<byte> writeBuffer = stackalloc byte[1];
        writeBuffer[0] = (byte)Register.Diagnostics5VRail;
        _device.WriteRead(writeBuffer, readBuffer);
        return BinaryPrimitives.ReadInt16LittleEndian(readBuffer) / 1000.0;
    }

    public double GetRtcBatteryVoltage()
    {
        Span<byte> readBuffer = stackalloc byte[2];
        Span<byte> writeBuffer = stackalloc byte[1];
        writeBuffer[0] = (byte)Register.BatteryVoltage;
        _device.WriteRead(writeBuffer, readBuffer);
        return BinaryPrimitives.ReadInt16LittleEndian(readBuffer) / 1000.0;
    }

    public Version GetFirmwareVersion()
    {
        Span<byte> readBuffer = stackalloc byte[1];
        Span<byte> writeBuffer = stackalloc byte[1];

        writeBuffer[0] = (byte)Register.FirmwareMajor;
        _device.WriteRead(writeBuffer, readBuffer);
        int major = readBuffer[0];

        writeBuffer[0] = (byte)Register.FirmwareMinor;
        _device.WriteRead(writeBuffer, readBuffer);
        int minor = readBuffer[0];

        return new(major, minor);
    }

    public DateTime GetDateTime()
    {
        Span<byte> readBuffer = stackalloc byte[6];
        Span<byte> writeBuffer = stackalloc byte[1];

        writeBuffer[0] = (byte)Register.GetRtc;
        _device.WriteRead(writeBuffer, readBuffer);

        var year = 2000 + readBuffer[0];
        var month = readBuffer[1];
        var day = readBuffer[2];
        var hour = readBuffer[3];
        var minute = readBuffer[4];
        var second = readBuffer[5];
        return new(year, month, day, hour, minute, second, DateTimeKind.Utc);
    }

    public void SetDateTime(DateTime datetime)
    {
        datetime = datetime.ToUniversalTime();
        Span<byte> writeBuffer = stackalloc byte[8]
        {
            (byte) Register.SetRtc,
            (byte) (datetime.Year - 2000),
            (byte) datetime.Month,
            (byte) datetime.Day,
            (byte) datetime.Hour,
            (byte) datetime.Minute,
            (byte) datetime.Second,
            CalibrationKey,
        };
        _device.Write(writeBuffer);
    }

    private static Register LookupAnalogOutputRegister(AnalogChannel channel) => channel switch
    {
        AnalogChannel.One => Register.AnalogOutChannel1,
        AnalogChannel.Two => Register.AnalogOutChannel2,
        AnalogChannel.Three => Register.AnalogOutChannel3,
        AnalogChannel.Four => Register.AnalogOutChannel4,
        _ => throw new ArgumentOutOfRangeException(nameof(channel)),
    };

    public void SetAnalogOut(AnalogChannel channel, double value)
    {
        var millivolts = (int)Math.Ceiling(value * 1000);
        millivolts = Math.Clamp(millivolts, 0, 10000);

        Span<byte> writeBuffer = stackalloc byte[3]
        {
            (byte)LookupAnalogOutputRegister(channel),
            (byte)(millivolts & 0x00ff),
            (byte)((millivolts & 0xff00) >> 8),
        };
        _device.Write(writeBuffer);
    }

    public double GetAnalogOut(AnalogChannel channel)
    {
        Span<byte> readBuffer = stackalloc byte[2];
        Span<byte> writeBuffer = stackalloc byte[1]
        {
            (byte)LookupAnalogOutputRegister(channel)
        };

        _device.WriteRead(writeBuffer, readBuffer);

        int millivolts = (readBuffer[1] << 8) + readBuffer[0];
        return millivolts / 1000.0;
    }
}