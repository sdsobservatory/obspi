namespace Obspi.Devices;

public interface IObspiInputs
{
    bool RoofOpen { get; set; }
    bool RoofClosed { get; set; }
    bool CloudWatcherSafe { get; set; }
    bool Tilt1 { get; set; }
    bool Tilt1Inverted { get; set; }
    bool Tilt2 { get; set; }
    bool Tilt2Inverted { get; set; }
    bool Tilt3 { get; set; }
    bool Tilt3Inverted { get; set; }
    bool Tilt4 { get; set; }
    bool Tilt4Inverted { get; set; }
    bool Input11 { get; set; }
    bool Input12 { get; set; }
    bool Input13 { get; set; }
    bool Input14 { get; set; }
    bool Input15 { get; set; }
}