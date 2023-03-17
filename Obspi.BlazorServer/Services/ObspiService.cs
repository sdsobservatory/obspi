using Obspi.Common.Dto;

namespace Obspi.BlazorServer.Services;

public class ObspiService : IObspiService
{
    private readonly HttpClient _httpClient;

    public ObspiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<IoDto>?> GetOutputs()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<IoDto>>("io/outputs");
    }

    public async Task<IEnumerable<IoDto>?> GetInputs()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<IoDto>>("io/inputs");
    }

    public async Task SetOutput(string name, bool value)
    {
        var response = await _httpClient.PostAsync($"io/outputs/{name}?state={value}", null);
        response.EnsureSuccessStatusCode();
    }
}
