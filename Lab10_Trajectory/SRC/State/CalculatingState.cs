using TrajectoryMovementLab10.Models;

namespace TrajectoryMovementLab10.State;

/// <summary>Промежуточное состояние выполнения вычислений; изменение параметров запрещено.</summary>
public sealed class CalculatingState : TrajectoryState
{
    public override string Name => "Выполняется расчёт";

    public override void Complete(TrajectorySession session, TrajectoryResult result)
    {
        session.ApplyResult(result);
        session.TransitionTo(new CompletedState());
    }

    public override void Fail(TrajectorySession session, string error)
    {
        session.ApplyError(error);
        session.TransitionTo(new FailedState());
    }
}
