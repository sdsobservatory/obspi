using System.Device.I2c;

namespace Obspi.Devices.I2c;

public class ThreadSafeI2cDevice : II2cDevice
{
    private readonly I2cDevice _device;
    private readonly I2cLock _semaphore;

    public I2cConnectionSettings ConnectionSettings => _device.ConnectionSettings;

    public ThreadSafeI2cDevice(int bus, int address, I2cLock semaphore)
    {
        _device = I2cDevice.Create(new(bus, address));
        _semaphore = semaphore;
    }

    public void Dispose()
    {
        _semaphore.Wait();
        
        try
        {
            _device.Dispose();
        }
        finally
        {
            _semaphore.Release();
        }
        
        _semaphore.Dispose();
        GC.SuppressFinalize(this);
    }

    public byte ReadByte()
    {
        _semaphore.Wait();
        
        try
        {
            return _device.ReadByte();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void Read(Span<byte> buffer)
    {
        _semaphore.Wait();
        
        try
        {
            _device.Read(buffer);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void WriteByte(byte value)
    {
        _semaphore.Wait();
        
        try
        {
            _device.WriteByte(value);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void Write(ReadOnlySpan<byte> buffer)
    {
        _semaphore.Wait();
        
        try
        {
            _device.Write(buffer);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void WriteRead(ReadOnlySpan<byte> writeBuffer, Span<byte> readBuffer)
    {
        _semaphore.Wait();
        
        try
        {
            _device.WriteRead(writeBuffer, readBuffer);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}