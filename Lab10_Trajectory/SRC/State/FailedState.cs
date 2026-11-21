using TrajectoryMovementLab10.Models;

namespace TrajectoryMovementLab10.State;

/// <summary>Ошибка вычисления; допускается исправление параметров, повтор или сброс.</summary>
public sealed class FailedState : TrajectoryState
{
    public override string Name => "Ошибка расчёта";

    public override void Configure(TrajectorySession session, TrajectoryParameters parameters)
    {
        session.ApplyParameters(parameters);
        session.TransitionTo(new ReadyState());
    }

    public override void Start(TrajectorySession session) =>
        session.TransitionTo(new CalculatingState());

    public override void Reset(TrajectorySession session) => session.ResetToDraft();
}
