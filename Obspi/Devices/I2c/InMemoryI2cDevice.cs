using System.Device.I2c;

namespace Obspi.Devices.I2c;

public class InMemoryI2cDevice : II2cDevice
{
    private readonly I2cLock _semaphore;
    private byte _currentRegister;
    
    public I2cConnectionSettings ConnectionSettings { get; }

    public List<byte> Data { get; }

    public InMemoryI2cDevice(int bus, int address, I2cLock semaphore)
    {
        _semaphore = semaphore;
        ConnectionSettings = new(bus, address);
        Data = Enumerable.Repeat((byte)0, byte.MaxValue + 1).ToList();
    }

    public void Dispose()
    {
    }

    public byte ReadByte()
    {
        _semaphore.Wait();
        
        try
        {
            return Data[_currentRegister++];
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
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Data[_currentRegister++];
            }
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
            _currentRegister = value;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void Write(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length == 0) return;

        _semaphore.Wait();
        
        try
        {
            _currentRegister = buffer[0];
        
            for (int i = 1; i < buffer.Length; i++)
            {
                Data[_currentRegister++] = buffer[i];
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void WriteRead(ReadOnlySpan<byte> writeBuffer, Span<byte> readBuffer)
    {
        Write(writeBuffer);
        Read(readBuffer);
    }
}