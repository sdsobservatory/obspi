namespace Obspi.Devices;

public interface IObspiInputs
{
    bool? GetValueOrNull(string name);
    IObspiInputs ToSnapshot();
    List<string> Names { get; }
    bool RoofOpened { get; set; }
    bool RoofClosed { get; set; }
    bool CloudWatcherUnsafe { get; set; }
    bool TiltJoshSafe { get; set; }
    bool TiltAlexSafe { get; set; }
    bool NoRain { get; set; }
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