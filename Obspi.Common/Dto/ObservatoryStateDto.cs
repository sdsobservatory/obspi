namespace Obspi.Common.Dto;

public record ObservatoryStateDto
{
    public required bool CanRoofOpen { get; init; }
	public required bool CanRoofClose { get; init; }
	public required bool IsRoofOpen { get; init; }
    public required bool IsRoofClosed { get; init; }
}
