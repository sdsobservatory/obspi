namespace Obspi.Common.Dto;

public record IoDto
{
    public required string Name { get; set; }
    public required bool Value { get; set; }
}