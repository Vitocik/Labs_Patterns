namespace Lab7.TrajectoryCommandIterator.Command;

/// <summary>Инициатор: запускает команды и хранит стек для отмены.</summary>
public sealed class CommandInvoker
{
    private readonly Stack<ITrajectoryCommand> _history = new();

    public int ExecutedCommandCount => _history.Count;

    public void ExecuteCommand(ITrajectoryCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        command.Execute();
        _history.Push(command);
    }

    public bool UndoLastCommand()
    {
        if (!_history.TryPeek(out var command))
            return false;

        command.Undo();
        _history.Pop();
        return true;
    }
}
