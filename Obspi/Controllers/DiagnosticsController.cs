using Microsoft.AspNetCore.Mvc;
using Obspi.Common.Dto;

namespace Obspi.Controllers;

[ApiController]
[Route("api/diagnostics")]
public class DiagnosticsController : ControllerBase
{
    private readonly IObservatory _observatory;

    public DiagnosticsController(IObservatory observatory)
    {
        _observatory = observatory;
    }

    [HttpGet]
    public IActionResult GetDiagnostics()
    {
        var items = new List<DiagnosticsDto>
        {
            new() { Name = "CPU Temperature", Value = _observatory.IndustrialAutomation.GetCpuTemperature().ToString(), Unit = "°C" },
            new() { Name = "24V Rail", Value = _observatory.IndustrialAutomation.Get24VRailVoltage().ToString(), Unit = "V" },
            new() { Name = "5V Rail", Value = _observatory.IndustrialAutomation.Get5VRailVoltage().ToString(), Unit = "V" },
            new() { Name = "RTC Battery", Value = _observatory.IndustrialAutomation.GetRtcBatteryVoltage().ToString(), Unit = "V" },
            new() { Name = "RTC Date", Value = _observatory.IndustrialAutomation.GetDateTime().ToString("O"), Unit = "--" },
            new() { Name = "Firmware Version", Value = _observatory.IndustrialAutomation.GetFirmwareVersion().ToString(), Unit = "--" },
            new() { Name = "Loop Time", Value = _observatory.LoopTime.TotalMilliseconds.ToString("F3"), Unit = "ms" },
        };

        return Ok(items);
    }
}