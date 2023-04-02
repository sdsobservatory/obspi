using Obspi.Common.Dto;

namespace Obspi.BlazorServer.Services;

public interface IObspiService
{
    Task<IEnumerable<IoDto>?> GetInputs();
    Task<IEnumerable<IoDto>?> GetOutputs();
    Task SetOutput(string name, bool value);
    Task<IEnumerable<DiagnosticsDto>?> GetDiagnostics();
    Task<ObservatoryStateDto?> GetObservatoryState();
	Task ToggleRoof();
    Task RestartPier1AC();
	Task RestartPier1DC();
	Task RestartPier2AC();
	Task RestartPier2DC();
	Task RestartPier3AC();
	Task RestartPier3DC();
	Task RestartPier4AC();
	Task RestartPier4DC();
	Task Restart10Micron1();
	Task Restart10Micron2();
}