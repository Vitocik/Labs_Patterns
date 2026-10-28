using TrajectoryMovementLab.Models;

namespace TrajectoryMovementLab.Bridge;

/// <summary>
/// Реализация (Implementor) паттерна «Мост».
/// Конкретная физическая модель возвращает общий формат результата.
/// </summary>
public interface IFlightModel
{
    string Name { get; }
    TrajectoryResult Calculate(TrajectoryParameters parameters);
}
