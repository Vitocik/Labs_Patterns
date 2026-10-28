using TrajectoryMovementLab.Models;

namespace TrajectoryMovementLab.Bridge;

/// <summary>Конкретный Implementor: ветер изменяет горизонтальную скорость.</summary>
public sealed class WindAffectedModel : IFlightModel
{
    public string Name => "Модель с постоянным ветром";

    public TrajectoryResult Calculate(TrajectoryParameters parameters) =>
        TrajectoryMath.Calculate(parameters, Name, parameters.WindSpeed);
}
