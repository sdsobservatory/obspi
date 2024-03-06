using Microsoft.Extensions.Options;
using Obspi.Common.Dto;
using System.Net.Sockets;
using System.Text;

namespace Obspi.Devices;

public record SqmOptions
{
    public const string Sqm = "Sqm";

    public required string Hostname { get; init; }
    public required int Port { get; init; }
}

public interface ISqmLe
{
    Task<SqmReading> GetReadingAsync(CancellationToken token);
}

public class SqmLe : ISqmLe
{
    private readonly IOptions<SqmOptions> _options;
    private const int ReadingLength = 57;

    public SqmLe(IOptions<SqmOptions> options)
    {
        _options = options;
    }

    public async Task<SqmReading> GetReadingAsync(CancellationToken token)
    {
        using var client = new TcpClient(_options.Value.Hostname, _options.Value.Port);
        await using var stream = client.GetStream();

        ReadOnlyMemory<byte> outBuffer = Encoding.ASCII.GetBytes("ux");
        Memory<byte> inBuffer = new byte[128];

        await stream.WriteAsync(outBuffer, token);
        await stream.ReadAtLeastAsync(inBuffer, ReadingLength, cancellationToken: token);
        string response = Encoding.ASCII.GetString(inBuffer.Span)[..^3];

        var reading = new SqmReading
        {
            Value = double.Parse(response[2..8]),
            Frequency = int.Parse(response[10..20]),
            PeriodCounts = int.Parse(response[23..31]),
            Period = TimeSpan.FromSeconds(double.Parse(response[35..46])),
            Temperature = double.Parse(response[48..54]),
            Timestamp = DateTime.UtcNow,
        };

        return reading;
    }
}
