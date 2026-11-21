namespace Lab7.TrajectoryCommandIterator.Command;

/// <summary>Команда с поддержкой выполнения и отмены.</summary>
public interface ITrajectoryCommand
{
    string Description { get; }
    void Execute();
    void Undo();
}
