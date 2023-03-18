namespace Obspi.Common.Dto;

public record DiagnosticsDto
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required string? Unit { get; init; }
}
