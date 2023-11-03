namespace Obspi.Common.Dto;

public record ObservatoryStateDto
{
    public required bool IsRoofSafeToMove { get; init; }
	public required bool IsRoofOpen { get; init; }
    public required bool IsRoofClosed { get; init; }
}
