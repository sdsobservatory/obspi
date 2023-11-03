namespace Obspi.Devices;

public interface IObspiInputs
{
    bool RoofOpened { get; set; }
    bool RoofClosed { get; set; }
    bool CloudWatcherUnsafe { get; set; }
    bool TiltJosh { get; set; }
    bool TiltAlex { get; set; }
    bool Input6 { get; set; }
    bool Input7 { get; set; }
    bool Input8 { get; set; }
    bool Input9 { get; set; }
    bool Input10 { get; set; }
    bool Input11 { get; set; }
    bool Input12 { get; set; }
    bool Input13 { get; set; }
    bool Input14 { get; set; }
    bool Input15 { get; set; }
    bool Input16 { get; set; }
}