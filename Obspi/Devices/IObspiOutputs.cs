namespace Obspi.Devices;

public interface IObspiOutputs
{
    bool Suicide { get; set; }
    bool RoofOpen { get; set; }
    bool RoofClose { get; set; }
    bool Josh1ACReset { get; set; }
    bool Josh1DCReset { get; set; }
    bool Josh2ACReset { get; set; }
    bool Josh2DCReset { get; set; }
    bool AlexACReset { get; set; }
    bool AlexDCReset { get; set; }
    bool CharlieACReset { get; set; }
    bool CharlieDCReset { get; set; }
    bool Josh10MicronReset { get; set; }
    bool Alex10MicronReset { get; set; }
    bool Bank0Channel14 { get; set; }
    bool Bank0Channel15 { get; set; }
    bool Bank0Channel16 { get; set; }
    bool Bank1Channel1 { get; set; }
    bool Bank1Channel2 { get; set; }
    bool Bank1Channel3 { get; set; }
    bool Bank1Channel4 { get; set; }
    bool Bank1Channel5 { get; set; }
    bool Bank1Channel6 { get; set; }
    bool Bank1Channel7 { get; set; }
    bool Bank1Channel8 { get; set; }
    bool Bank1Channel9 { get; set; }
    bool Bank1Channel10 { get; set; }
    bool Bank1Channel11 { get; set; }
    bool Bank1Channel12 { get; set; }
    bool Bank1Channel13 { get; set; }
    bool Bank1Channel14 { get; set; }
    bool Bank1Channel15 { get; set; }
    bool Bank1Channel16 { get; set; }
}