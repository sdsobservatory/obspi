using Obspi.Common;

namespace Obspi.Commands;

public abstract class Command
{
    public Guid Id { get; } = Guid.NewGuid();

    public CommandState State { get; private set; } = CommandState.Created;

    protected abstract void Execute(Observatory observatory);

    public void Run(Observatory observatory)
    {
        try
        {
            State = CommandState.Running;
            Execute(observatory);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            State = CommandState.Complete;
        }
    }

    public override string ToString()
    {
        return GetType().Name;
    }
}