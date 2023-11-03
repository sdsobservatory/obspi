namespace Obspi.Common.Dto;

public record WeatherDto
{
    /// <summary>
    /// Get a timestamp when the measurement was taken.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Get the cloud cover percent, 0 to 100.
    /// </summary>
    public required double CloudCover { get; init; }

    /// <summary>
    /// Get the dew point, in degrees C.
    /// </summary>
    public required double DewPoint { get; init; }

    /// <summary>
    /// Get the humidity percent, 0 to 100.
    /// </summary>
    public required double Humidity { get; init; }

    /// <summary>
    /// Get the pressure, in mmbar.
    /// </summary>
    public required double Pressure { get; init; }

    /// <summary>
    /// Get the sky brightness, in lux.
    /// </summary>
    public required double SkyBrightness { get; init; }

    /// <summary>
    /// Get the sky quality, in mag/arcsec^2.
    /// </summary>
    public required double SkyQuality { get; init; }

    /// <summary>
    /// Get the sky temperature, in degrees C.
    /// </summary>
    public required double SkyTemperature { get; init; }

    /// <summary>
    /// Get the air temperature, in degrees C.
    /// </summary>
    public required double Temperature { get; init; }

    /// <summary>
    /// Get the wind speed, in mph.
    /// </summary>
    public required double WindSpeed { get; init; }
}
