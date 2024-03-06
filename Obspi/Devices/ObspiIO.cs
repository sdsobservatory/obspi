namespace Obspi.Devices;

public interface IObspiIO
{
    IObspiInputs Inputs { get; }
    IObspiOutputs Outputs { get; }
}

public class ObspiIO : IObspiIO
{
    public ObspiIO(IList<InputBank16> inputs, IList<OutputBank16> outputs)
    {
        Inputs = new ObspiInputs(inputs);
        Outputs = new ObspiOutputs(outputs);
    }
    
    public IObspiInputs Inputs { get; }
    public IObspiOutputs Outputs { get; }
}
