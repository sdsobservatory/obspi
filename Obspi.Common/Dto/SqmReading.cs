namespace Obspi.Common.Dto;

public record SqmReading
{
    public required double Value { get; init; }
    public required int Frequency { get; init; }
    public required int PeriodCounts { get; init; }
    public required TimeSpan Period { get; init; }
    public required double Temperature { get; init; }
    public required DateTime Timestamp { get; init; }
}
