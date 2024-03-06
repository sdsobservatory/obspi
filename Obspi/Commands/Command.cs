using Nito.AsyncEx;
using Obspi.Common;

namespace Obspi.Commands;

public abstract class Command
{
    private readonly AsyncManualResetEvent _completeEvent = new(false);
    
    public Guid Id { get; } = Guid.NewGuid();

    public CommandState State { get; private set; } = CommandState.Created;

    protected abstract Task Execute(IObservatory observatory, CancellationToken token);

    public async Task Run(IObservatory observatory, CancellationToken token)
    {
        try
        {
            State = CommandState.Running;
            await Execute(observatory, token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            State = CommandState.Complete;
            _completeEvent.Set();
        }
    }

    internal void SetComplete() => _completeEvent.Set();

    public void Wait(CancellationToken token = default) => _completeEvent.Wait(token);

    public Task WaitAsync(CancellationToken token = default) => _completeEvent.WaitAsync(token);

    public override string ToString()
    {
        return GetType().Name;
    }
}