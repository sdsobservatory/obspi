using Obspi.Devices;
using Obspi.Common.Dto;
using Microsoft.Extensions.Options;
using Obspi.Config;
using Flurl.Http;
using Flurl;

namespace Obspi.Services;

public class WeatherService
{
    private readonly WeatherOptions _options;
    private readonly SqmLe _sqm;

    public WeatherService(IOptions<WeatherOptions> options, SqmLe sqm)
    {
        _options = options.Value;
        _sqm = sqm;
    }

    public async Task<WeatherDto> GetWeatherAsync(CancellationToken token)
    {
        var weatherTask = new Url(_options.AagExporterUrl).GetJsonAsync<AagData>();
        var sqmTask = _sqm.GetReadingAsync(token);

        await Task.WhenAll(weatherTask, sqmTask);

        var weatherData = weatherTask.Result;
        var sqmDat = sqmTask.Result;

        // TODO: Get the other weather data from the cloud watcher or other sensors

        return new WeatherDto
        {
            CloudCover = 0,
            DewPoint = weatherData.DewPoint,
            Humidity = weatherData.Humidity,
            Pressure = weatherData.AbsolutePressure,
            SkyBrightness = 108000 * Math.Pow(10, -0.4 * sqmDat.Value),
            SkyQuality = sqmDat.Value,
            SkyTemperature = weatherData.RawInfrared,
            Temperature = weatherData.Temperature,
            WindSpeed = weatherData.Wind,
        };
    }
}
