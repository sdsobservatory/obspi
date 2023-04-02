using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Options;
using Obspi.BlazorServer.Options;
using Obspi.Common;
using Obspi.Common.Dto;

namespace Obspi.BlazorServer.Services;

public class ObspiService : IObspiService
{
    private readonly IFlurlClient _client;

    public ObspiService(IFlurlClientFactory clientFactory, IOptions<ObspiOptions> options)
    {
		_client = clientFactory.Get(options.Value.BaseUrl);
    }

    public async Task<IEnumerable<IoDto>?> GetOutputs()
    {
		return await _client
			.Request("io", "outputs")
			.GetJsonAsync<IEnumerable<IoDto>>();
    }

    public async Task<IEnumerable<IoDto>?> GetInputs()
    {
		return await _client
			.Request("io", "inputs")
			.GetJsonAsync<IEnumerable<IoDto>>();
    }

    public async Task SetOutput(string name, bool value)
    {
		await _client
			.Request("io", "outputs", name)
			.SetQueryParam("state", value)
			.PostAsync();
    }

    public async Task<IEnumerable<DiagnosticsDto>?> GetDiagnostics()
    {
		return await _client
			.Request("diagnostics")
			.GetJsonAsync<IEnumerable<DiagnosticsDto>>();
    }

    public async Task<ObservatoryStateDto?> GetObservatoryState()
    {
		return await _client
			.Request("observatory")
			.GetJsonAsync<ObservatoryStateDto>();
	}

    public async Task<CommandState> GetCommandState(Guid id)
    {
		var result = await _client
			.Request("observatory", "command", "status", id)
			.GetJsonAsync<CommandStateDto>();
		return result.State;
    }

    private async Task WaitForCommand(string uri)
    {
		var id = await EnqueueCommand(uri);
		var state = await GetCommandState(id);
		using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(120));

		while (state != CommandState.Complete && !timeout.IsCancellationRequested)
		{
			await Task.Delay(200);
			state = await GetCommandState(id);
		}

		async Task<Guid> EnqueueCommand(string uri)
		{
			var response = await _client.Request(uri).PostAsync();
			var dto = await response.GetJsonAsync<CommandQueuedDto>();
			return dto.Id;
		}
	}

    public async Task ToggleRoof()
    {
        await WaitForCommand("observatory/command/toggle_roof");
    }

    public async Task RestartPier1AC()
	{
		await WaitForCommand("observatory/command/restart_pier1_ac");
	}

	public async Task RestartPier1DC()
	{
		await WaitForCommand("observatory/command/restart_pier1_dc");
	}

	public async Task RestartPier2AC()
	{
		await WaitForCommand("observatory/command/restart_pier2_ac");
	}

	public async Task RestartPier2DC()
	{
		await WaitForCommand("observatory/command/restart_pier2_dc");
	}

	public async Task RestartPier3AC()
	{
		await WaitForCommand("observatory/command/restart_pier3_ac");
	}

	public async Task RestartPier3DC()
	{
		await WaitForCommand("observatory/command/restart_pier3_dc");
	}

	public async Task RestartPier4AC()
	{
		await WaitForCommand("observatory/command/restart_pier4_ac");
	}

	public async Task RestartPier4DC()
	{
		await WaitForCommand("observatory/command/restart_pier4_dc");
	}

	public async Task Restart10Micron1()
	{
		await WaitForCommand("observatory/command/restart_10micron_1");
	}

	public async Task Restart10Micron2()
	{
		await WaitForCommand("observatory/command/restart_10micron_2");
	}
}
