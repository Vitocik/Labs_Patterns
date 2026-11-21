using TrajectoryMovementLab9.Models;

namespace TrajectoryMovementLab9.Memento;

/// <summary>
/// Неизменяемый снимок состояния сессии. Его внутренние данные доступны только
/// Originator внутри сборки; объект истории может лишь хранить и возвращать снимки.
/// </summary>
public sealed class TrajectoryMemento
{
    private readonly TrajectoryParameters _parameters;
    private readonly TrajectoryResult? _result;

    internal TrajectoryMemento(
        TrajectoryParameters parameters,
        TrajectoryResult? result,
        DateTime savedAt)
    {
        _parameters = parameters;
        _result = result;
        SavedAt = savedAt;
    }

    internal TrajectoryParameters Parameters => _parameters;
    internal TrajectoryResult? Result => _result;
    public DateTime SavedAt { get; }

    public string Description => _result is null
        ? $"{_parameters.Name} (параметры без результата)"
        : $"{_parameters.Name}: дальность {_result.Range:F3} м";
}
