namespace Obspi.Devices;

public class ObspiIO
{
    public ObspiIO(IList<InputBank16> inputs, IList<OutputBank16> outputs)
    {
        Inputs = new(inputs);
        Outputs = new(outputs);
    }
    
    public ObspiInputs Inputs { get; }
    public ObspiOutputs Outputs { get; }
}
