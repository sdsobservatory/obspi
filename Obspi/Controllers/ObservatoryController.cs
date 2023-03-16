using Microsoft.AspNetCore.Mvc;
using Obspi.Commands;

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
        return Ok(new
        {
            CanRoofMove = _observatory.CanRoofMove,
            IsRoofOpen = _observatory.IsRoofOpen,
            IsRoofClosed = _observatory.IsRoofClosed,
        });
    }

    [HttpPost("command/toggle_roof")]
    public IActionResult ToggleRoof()
    {
        _observatory.EnqueueCommand(new PulsedIoCommand(TimeSpan.FromSeconds(3), true, x => x.RoofMotor));
        return Accepted();
    }
}