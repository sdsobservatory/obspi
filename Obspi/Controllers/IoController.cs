using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Obspi.Common.Dto;
using Obspi.Config;

namespace Obspi.Controllers;

[ApiController]
[Route("api/io")]
public class IoController : ControllerBase
{
    private readonly IOptions<I2cDevicesOptions> _i2cOptions;
    private readonly Observatory _observatory;

    public IoController(IOptions<I2cDevicesOptions> i2cOptions, Observatory observatory)
    {
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
        bool success = _observatory.IO.Outputs.TrySetValue(name, state);
        return success ? AcceptedAtAction(nameof(GetOutputByName), "io", new { Name = name }) : BadRequest();
    }
}