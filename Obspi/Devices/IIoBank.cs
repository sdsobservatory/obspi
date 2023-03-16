namespace Obspi.Devices;

public interface IIoBank
{
    int Count { get; }

    int Value { get; set; }

    bool this[int i] { get; set; }
}