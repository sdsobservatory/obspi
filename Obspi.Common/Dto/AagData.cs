namespace Obspi.Common.Dto;

public record AagData
{
    public required DateTime Timestamp { get; init; }
    public required string CwInfo { get; init; }
    public required string SldData { get; init; }
    public required double Clouds { get; init; }
    public required bool CloudsSafe { get; init; }
    public required double Temperature { get; init; }
    public required double Wind { get; init; }
    public required bool WindSafe { get; init; }
    public required double Gust { get; init; }
    public required int Rain { get; init; }
    public required bool RainSafe { get; init; }
    public required int Light { get; init; }
    public required bool LightSafe { get; init; }
    public required bool Switch { get; init; }
    public required bool Safe { get; init; }
    public required double Humidity { get; init; }
    public required bool HumiditySafe { get; init; }
    public required double DewPoint { get; init; }
    public required double AbsolutePressure { get; init; }
    public required double RelativePressure { get; init; }
    public required bool PressureSafe { get; init; }
    public required double RawInfrared { get; init; }
}