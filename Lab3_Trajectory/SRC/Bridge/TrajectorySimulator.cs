using TrajectoryMovementLab.Models;

namespace TrajectoryMovementLab.Bridge;

/// <summary>
/// Абстракция паттерна «Мост». Тип представления отчёта отделён от физической модели.
/// </summary>
public abstract class TrajectorySimulator
{
    private readonly IFlightModel _model;

    protected TrajectorySimulator(IFlightModel model)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
    }

    public TrajectoryResult Simulate(TrajectoryParameters parameters)
    {
        TrajectoryResult result = _model.Calculate(parameters);
        Display(parameters, result);
        return result;
    }

    protected abstract void Display(
        TrajectoryParameters parameters,
        TrajectoryResult result);
}
