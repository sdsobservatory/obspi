using Obspi.Devices;
using Obspi.Common.Dto;

namespace Obspi.Services;

public class WeatherService
{
    private readonly SqmLe _sqm;

    public WeatherService(SqmLe sqm)
    {
        _sqm = sqm;
    }

    public async Task<WeatherDto> GetWeatherAsync(CancellationToken token)
    {
        var sqmReading = await _sqm.GetReadingAsync(token);

        // TODO: Get the other weather data from the cloud watcher or other sensors

        return new WeatherDto
        {
            CloudCover = 0,
            DewPoint = 0,
            Humidity = 0,
            Pressure = 0,
            SkyBrightness = 108000 * Math.Pow(10, -0.4 * sqmReading.Value),
            SkyQuality = sqmReading.Value,
            SkyTemperature = 0,
            Temperature = sqmReading.Temperature, // TODO: use cloudwatcher temperature
            WindSpeed = 0,
        };
    }
}
