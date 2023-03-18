using Obspi.Common.Dto;

namespace Obspi.BlazorServer.Services;

public interface IObspiService
{
    Task<IEnumerable<IoDto>?> GetInputs();
    Task<IEnumerable<IoDto>?> GetOutputs();
    Task SetOutput(string name, bool value);
    Task<IEnumerable<DiagnosticsDto>?> GetDiagnostics();
}