namespace Obspi.Config;

public class I2cDevicesOptions
{
    public const string I2cDevices = "I2cDevices";
    
    public required bool UseInMemoryI2c { get; init; }
    public required int Watchdog { get; init; }
    public required List<int> InputBanks { get; init; }
    public required List<int> OutputBanks { get; init; }
}