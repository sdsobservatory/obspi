namespace Obspi.Common.Dto;

public record CommandStateDto
{
    public required CommandState State { get; init; }
}
