using Lab7.TrajectoryCommandIterator.Models;

namespace Lab7.TrajectoryCommandIterator.Session;

/// <summary>Получатель команд: хранит результаты выполненных сценариев.</summary>
public sealed class TrajectorySession
{
    private readonly List<TrajectoryResult> _results = [];

    public IReadOnlyList<TrajectoryResult> Results => _results.AsReadOnly();

    public void Add(TrajectoryResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        _results.Add(result);
    }

    public void Remove(TrajectoryResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        if (!_results.Remove(result))
            throw new InvalidOperationException("Результат команды отсутствует в текущей сессии.");
    }
}
