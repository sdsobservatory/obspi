using Microsoft.AspNetCore.Mvc;

namespace Obspi.Controllers;

[ApiController]
[Route("api/diagnostics")]
public class DiagnosticsController : ControllerBase
{
    private readonly Observatory _observatory;

    public DiagnosticsController(Observatory observatory)
    {
        _observatory = observatory;
    }

    [HttpGet]
    public IActionResult GetDiagnostics()
    {
        return Ok(new
        {
            CpuTemperature = _observatory.IndustrialAutomation.GetCpuTemperature(),
            Voltage24 = _observatory.IndustrialAutomation.Get24VRailVoltage(),
            Voltage5 = _observatory.IndustrialAutomation.Get5VRailVoltage(),
            VoltageRtc = _observatory.IndustrialAutomation.GetRtcBatteryVoltage(),
            FirmwareVersion = _observatory.IndustrialAutomation.GetFirmwareVersion(),
            RtcDateTime = _observatory.IndustrialAutomation.GetDateTime(),
            LoopTimeSeconds = _observatory.LoopTime.TotalSeconds,
        });
    }
}