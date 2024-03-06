using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Obspi.Common.Dto;
using Obspi.Config;

namespace Obspi.Controllers;

[ApiController]
[Route("api/io")]
public class IoController : ControllerBase
{
    private readonly ILogger<IoController> _logger;
	private readonly IOptions<I2cDevicesOptions> _i2cOptions;
    private readonly IObservatory _observatory;

    public IoController(
        ILogger<IoController> logger,
        IOptions<I2cDevicesOptions> i2cOptions,
        IObservatory observatory)
    {
		_logger = logger;
		_i2cOptions = i2cOptions;
        _observatory = observatory;
    }
        
    [HttpGet("inputs")]
    public IActionResult GetInputs()
    {
        var items = _observatory.IO.Inputs
            .Names
            .Select(io => new IoDto { Name = io, Value = _observatory.IO.Inputs.GetValueOrNull(io) ?? false })
            .ToList();

        return Ok(items);
    }
    
    [HttpGet("outputs")]
    public IActionResult GetOutputs()
    {
        var items = _observatory.IO.Outputs
            .Names
            .Select(io => new IoDto { Name = io, Value = _observatory.IO.Outputs.GetValueOrNull(io) ?? false })
            .ToList();

        return Ok(items);
    }

    [HttpGet("inputs/{name}")]
    public IActionResult GetInputByName(string name)
    {
        bool? value = _observatory.IO.Inputs.GetValueOrNull(name);
        if (!value.HasValue)
            return NotFound();

        return Ok(new { State = value });
    }
    
    [HttpGet("outputs/{name}")]
    public IActionResult GetOutputByName(string name)
    {
        bool? value = _observatory.IO.Outputs.GetValueOrNull(name);
        if (!value.HasValue)
            return NotFound();

        return Ok(new { State = value });
    }

    [HttpPost("outputs/{name}")]
    public IActionResult SetOutputByName(string name, [FromQuery] bool state)
    {
        _logger.LogInformation("Setting output {Name} to {State}", name, state);
        bool success = _observatory.IO.Outputs.TrySetValue(name, state);
        return success ? AcceptedAtAction(nameof(GetOutputByName), "io", new { Name = name }) : BadRequest();
    }

    [HttpGet("analogoutput/{channel}")]
    public IActionResult GetAnalogOutput(int channel)
    {
        if (!Enum.IsDefined((Devices.IndustrialAutomation.AnalogChannel)channel))
            return BadRequest();

        var value = _observatory.IndustrialAutomation.GetAnalogOut((Devices.IndustrialAutomation.AnalogChannel)channel);
        return Ok(new { Value = value });
    }

    [HttpPost("analogoutput/{channel}")]
    public IActionResult SetAnalogOutput(int channel, [FromQuery] double value)
    {
        if (!Enum.IsDefined((Devices.IndustrialAutomation.AnalogChannel)channel))
            return BadRequest();

        _logger.LogInformation("Setting analog output channel {Channel} to {Value}", channel, value);
        _observatory.IndustrialAutomation.SetAnalogOut((Devices.IndustrialAutomation.AnalogChannel)channel, value);
        return Ok();
    }
}