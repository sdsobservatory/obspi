namespace Obspi.Devices.I2c;

public class I2cLock : IDisposable
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public void Dispose()
    {
        _semaphore.Dispose();
        GC.SuppressFinalize(this);
    }

    public void Wait()
    {
        _semaphore.Wait();
    }

    public void Release()
    {
        _semaphore.Release();
    }
}