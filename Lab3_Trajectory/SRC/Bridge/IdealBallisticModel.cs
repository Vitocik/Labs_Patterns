using TrajectoryMovementLab.Models;

namespace TrajectoryMovementLab.Bridge;

/// <summary>Конкретный Implementor: движение без горизонтального ветра.</summary>
public sealed class IdealBallisticModel : IFlightModel
{
    public string Name => "Идеальная баллистическая модель";

    public TrajectoryResult Calculate(TrajectoryParameters parameters) =>
        TrajectoryMath.Calculate(parameters, Name, horizontalWind: 0.0);
}
