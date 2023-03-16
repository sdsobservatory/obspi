using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
    public IActionResult GetInputNames()
    {
        return Ok(_observatory.IO.Inputs.Names);
    }
    
    [HttpGet("inputs/all")]
    public IActionResult GetInputs()
    {
        return Ok(_observatory.IO.Inputs.Names
            .ToDictionary(key => key, name => _observatory.IO.Inputs.GetValueOrNull(name)));
    }
    
    [HttpGet("outputs")]
    public IActionResult GetOutputNames()
    {
        return Ok(_observatory.IO.Outputs.Names);
    }
    
    [HttpGet("outputs/all")]
    public IActionResult GetOutputs()
    {
        return Ok(_observatory.IO.Outputs.Names
            .ToDictionary(key => key, name => _observatory.IO.Outputs.GetValueOrNull(name)));
    }

    [HttpGet("input/{name}")]
    public IActionResult GetInputByName(string name)
    {
        bool? value = _observatory.IO.Inputs.GetValueOrNull(name);
        if (!value.HasValue)
            return NotFound();

        return Ok(new { State = value });
    }
    
    [HttpGet("output/{name}")]
    public IActionResult GetOutputByName(string name)
    {
        bool? value = _observatory.IO.Outputs.GetValueOrNull(name);
        if (!value.HasValue)
            return NotFound();

        return Ok(new { State = value });
    }

    [HttpPost("output/{name}")]
    public IActionResult SetOutputByName(string name, [FromQuery] bool state)
    {
        bool success = _observatory.IO.Outputs.TrySetValue(name, state);
        return success ? AcceptedAtAction(nameof(GetOutputByName), "io", new { Name = name }) : BadRequest();
    }
}