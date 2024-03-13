namespace Obspi.Common.Dto;

public record ObservatoryStateDto
{
    public required bool IsRoofSafeToMove { get; init; }
	public required bool IsRoofOpen { get; init; }
    public required bool IsRoofClosed { get; init; }
    public required bool IsAutoRoofEnabled { get; init; }
    public required string SunriseTime { get; init; }
    public required string SunriseNormalAlertTime { get; init; }
    public required string SunriseEmergencyAlertTime { get; init; }
}
