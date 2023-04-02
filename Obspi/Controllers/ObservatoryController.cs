using Microsoft.AspNetCore.Mvc;
using Obspi.Commands;
using Obspi.Common.Dto;

namespace Obspi.Controllers;

[ApiController]
[Route("api/observatory")]
public class ObservatoryController : ControllerBase
{
    private readonly Observatory _observatory;

    public ObservatoryController(Observatory observatory)
    {
        _observatory = observatory;
    }

    [HttpGet]
    public IActionResult GetState()
    {
        var dto = new ObservatoryStateDto
        {
            CanRoofOpen = _observatory.CanRoofOpen,
			CanRoofClose = _observatory.CanRoofClose,
			IsRoofOpen = _observatory.IsRoofOpen,
            IsRoofClosed = _observatory.IsRoofClosed,
        };

        return Ok(dto);
    }

    [HttpGet("command/status/{id}")]
    public IActionResult GetCommandStatus(Guid id)
    {
        var cmd = _observatory.Commands.FirstOrDefault(x => x.Id == id);
        var state = cmd?.State ?? Common.CommandState.Complete;
        var dto = new CommandStateDto { State = state };
        return Ok(dto);
    }

    [HttpPost("command/toggle_roof")]
    public IActionResult ToggleRoof()
    {
        // TODO: Some kind of standardized json format that has an error string
        if (!(_observatory.CanRoofOpen || _observatory.CanRoofClose))
            return BadRequest();

        var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.RoofMotor);
        _observatory.EnqueueCommand(cmd);
        return Accepted(new CommandQueuedDto { Id = cmd.Id });
    }
    
    [HttpPost("command/restart_pier1_ac")]
	public IActionResult RestartPier1AC()
    {
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Pier1AC);
		_observatory.EnqueueCommand(cmd);
		return Accepted(new CommandQueuedDto { Id = cmd.Id });
	}

	[HttpPost("command/restart_pier1_dc")]
	public IActionResult RestartPier1DC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Pier1DC);
		_observatory.EnqueueCommand(cmd);
		return Accepted(new CommandQueuedDto { Id = cmd.Id });
	}

	[HttpPost("command/restart_pier2_ac")]
	public IActionResult RestartPier2AC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Pier2AC);
		_observatory.EnqueueCommand(cmd);
		return Accepted(new CommandQueuedDto { Id = cmd.Id });
	}

	[HttpPost("command/restart_pier2_dc")]
	public IActionResult RestartPier2DC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Pier2DC);
		_observatory.EnqueueCommand(cmd);
		return Accepted(new CommandQueuedDto { Id = cmd.Id });
	}

	[HttpPost("command/restart_pier3_ac")]
	public IActionResult RestartPier3AC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Pier3AC);
		_observatory.EnqueueCommand(cmd);
		return Accepted(new CommandQueuedDto { Id = cmd.Id });
	}

	[HttpPost("command/restart_pier3_dc")]
	public IActionResult RestartPier3DC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Pier3DC);
		_observatory.EnqueueCommand(cmd);
		return Accepted(new CommandQueuedDto { Id = cmd.Id });
	}

	[HttpPost("command/restart_pier4_ac")]
	public IActionResult RestartPier4AC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Pier4AC);
		_observatory.EnqueueCommand(cmd);
		return Accepted(new CommandQueuedDto { Id = cmd.Id });
	}

	[HttpPost("command/restart_pier4_dc")]
	public IActionResult RestartPier4DC()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.Pier4DC);
		_observatory.EnqueueCommand(cmd);
		return Accepted(new CommandQueuedDto { Id = cmd.Id });
	}

	[HttpPost("command/restart_10micron_1")]
	public IActionResult Restart10Micron1()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.TenMicron1);
		_observatory.EnqueueCommand(cmd);
		return Accepted(new CommandQueuedDto { Id = cmd.Id });
	}

	[HttpPost("command/restart_10micron_2")]
	public IActionResult Restart10Micron2()
	{
		var cmd = new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.TenMicron2);
		_observatory.EnqueueCommand(cmd);
		return Accepted(new CommandQueuedDto { Id = cmd.Id });
	}
}