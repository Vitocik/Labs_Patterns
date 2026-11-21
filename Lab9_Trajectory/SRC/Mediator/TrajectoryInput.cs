using TrajectoryMovementLab9.Models;

namespace TrajectoryMovementLab9.Mediator;

/// <summary>Коллега, принимающий исходные параметры пользователя.</summary>
public sealed class TrajectoryInput(ITrajectoryMediator mediator)
{
    public void Submit(TrajectoryParameters parameters) => mediator.ParametersSubmitted(parameters);
    public void Calculate() => mediator.CalculationRequested();
}
