using System.Globalization;
using System.Text.Json.Serialization;

namespace Obspi.Devices;

public record AagCloudWatcherData
{
    [JsonPropertyName("dateLocalTime")]
    private string TimestampJson { get; set; } = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);

    public DateTime Timestamp
    {
        get => DateTime.ParseExact(TimestampJson, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
        set => TimestampJson = value.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
    }

    [JsonPropertyName("cwinfo")]
    public string CloudWatcherInfo { get; set; } = string.Empty;

    [JsonPropertyName("slddata")]
    public string BoltwoodFormat { get; set; } = string.Empty;

    [JsonPropertyName("clouds")]
    public double SkyTemperature { get; set; }

    [JsonPropertyName("temp")]
    public double Temperature { get; set; }

    [JsonPropertyName("wind")]
    public double Wind { get; set; }

    [JsonPropertyName("gust")]
    public double Gust { get; set; }

    [JsonPropertyName("rain")]
    public double Rain { get; set; }

    [JsonPropertyName("light")]
    public double Light { get; set; }

    [JsonPropertyName("switch")]
    public bool Switch { get; set; }

    [JsonPropertyName("safe")]
    public bool Safe { get; set; }

    [JsonPropertyName("hum")]
    public double Humidity { get; set; }

    [JsonPropertyName("dewp")]
    public double DewPoint { get; set; }

    [JsonPropertyName("abspress")]
    public double AbsolutePressure { get; set; }
    
    [JsonPropertyName("relpress")]
    public double RelativePressure { get; set; }

    [JsonPropertyName("rawir")]
    public double RawIR { get; set; }
}

public interface ICloudWatcher
{
    AagCloudWatcherData MostRecentData { get; set; }
}

public class CloudWatcher : ICloudWatcher
{
    public AagCloudWatcherData MostRecentData { get; set; } = new();

    public CloudWatcher()
    {
    }
}
