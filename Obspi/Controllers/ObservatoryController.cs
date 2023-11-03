using Microsoft.AspNetCore.Mvc;
using Obspi.Commands;
using Obspi.Common.Dto;
using Obspi.Devices;
using Obspi.Services;

namespace Obspi.Controllers;

[ApiController]
[Route("api/observatory")]
public class ObservatoryController : ControllerBase
{
    private readonly Observatory _observatory;
	private readonly WeatherService _weather;

    public ObservatoryController(Observatory observatory, WeatherService weather)
    {
        _observatory = observatory;
		_weather = weather;
    }

    [HttpGet]
    public IActionResult GetState()
    {
        var dto = new ObservatoryStateDto
        {
			IsRoofSafeToMove = _observatory.IsRoofSafeToMove,
			IsRoofOpen = _observatory.IsRoofOpen,
            IsRoofClosed = _observatory.IsRoofClosed,
        };

        return Ok(dto);
    }

	#region Commands

    [HttpPost("command/open_roof")]
	public async Task<IActionResult> OpenRoof()
	{
        // TODO: Some kind of standardized json format that has an error string
        if (!_observatory.IsRoofSafeToMove)
            return BadRequest();

		var cmd = new OpenRoofCommand
		{
			Timeout = TimeSpan.FromSeconds(90),
			OnTimeout = () =>
			{
				// TODO: notification system
                Console.WriteLine("Opening roof timed out!");
            }
		};

        _observatory.RoofCts?.Dispose();
        _observatory.RoofCts = new();
        _observatory.EnqueueCommand(cmd, _observatory.RoofCts.Token);
        await cmd.WaitAsync();
        _observatory.RoofCts?.Dispose();
		_observatory.RoofCts = null;
        return Ok();
	}

    [HttpPost("command/close_roof")]
    public async Task<IActionResult> CloseRoof()
    {
        // TODO: Some kind of standardized json format that has an error string
        if (!_observatory.IsRoofSafeToMove)
            return BadRequest();

        var cmd = new CloseRoofCommand
        {
            Timeout = TimeSpan.FromSeconds(90),
            OnTimeout = () =>
            {
				// TODO: notification system
                Console.WriteLine("Closing roof timed out!");
            }
        };

        _observatory.RoofCts?.Dispose();
        _observatory.RoofCts = new();
        _observatory.EnqueueCommand(cmd, _observatory.RoofCts.Token);
        await cmd.WaitAsync();
        _observatory.RoofCts?.Dispose();
        _observatory.RoofCts = null;
        return Ok();
    }

	[HttpPost("command/stop_roof")]
	public IActionResult StopRoof()
	{
		_observatory.RoofCts?.Cancel();
		return Ok();
	}

    [HttpPost("command/restart_josh1_ac")]
	public async Task<IActionResult> RestartPier1AC()
    {
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Josh1ACReset);
		_observatory.EnqueueCommand(cmd);
        await cmd.WaitAsync();
        return Ok();
    }

	[HttpPost("command/restart_josh1_dc")]
	public async Task<IActionResult> RestartPier1DC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Josh1DCReset);
		_observatory.EnqueueCommand(cmd);
        await cmd.WaitAsync();
        return Ok();
    }

	[HttpPost("command/restart_josh2_ac")]
	public async Task<IActionResult> RestartPier2AC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Josh2ACReset);
		_observatory.EnqueueCommand(cmd);
        await cmd.WaitAsync();
        return Ok();
    }

	[HttpPost("command/restart_josh2_dc")]
	public async Task<IActionResult> RestartPier2DC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Josh2DCReset);
		_observatory.EnqueueCommand(cmd);
        await cmd.WaitAsync();
        return Ok();
    }

	[HttpPost("command/restart_alex_ac")]
	public async Task<IActionResult> RestartPier3AC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.AlexACReset);
		_observatory.EnqueueCommand(cmd);
        await cmd.WaitAsync();
        return Ok();
    }

	[HttpPost("command/restart_alex_dc")]
	public async Task<IActionResult> RestartPier3DC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.AlexDCReset);
		_observatory.EnqueueCommand(cmd);
        await cmd.WaitAsync();
        return Ok();
    }

	[HttpPost("command/restart_charlie_ac")]
	public async Task<IActionResult> RestartPier4AC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.CharlieACReset);
		_observatory.EnqueueCommand(cmd);
        await cmd.WaitAsync();
        return Ok();
    }

	[HttpPost("command/restart_charlie_dc")]
	public async Task<IActionResult> RestartPier4DC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.CharlieDCReset);
		_observatory.EnqueueCommand(cmd);
        await cmd.WaitAsync();
        return Ok();
    }

	[HttpPost("command/restart_josh_10micron")]
	public async Task<IActionResult> Restart10Micron1()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Josh10MicronReset);
		_observatory.EnqueueCommand(cmd);
		await cmd.WaitAsync();
		return Ok();
	}

	[HttpPost("command/restart_alex_10micron")]
	public async Task<IActionResult> Restart10Micron2()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Alex10MicronReset);
		_observatory.EnqueueCommand(cmd);
        await cmd.WaitAsync();
        return Ok();
    }

	#endregion

	#region Weather

	[HttpGet("weather")]
	public async Task<IActionResult> GetWeather(CancellationToken token)
	{
		var report = await _weather.GetWeatherAsync(token);
		return Ok(report);
	}

	[HttpPut("aag")]
	public IActionResult PutAagCloudWatherData([FromBody] AagCloudWatcherData data)
	{
		_observatory.CloudWatcher.MostRecentData = data;
		return Ok();
	}

	#endregion
}