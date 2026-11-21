using TrajectoryMovementLab10.Models;

namespace TrajectoryMovementLab10.State;

/// <summary>Начальное состояние: параметры ещё не заданы.</summary>
public sealed class DraftState : TrajectoryState
{
    public override string Name => "Черновик";

    public override void Configure(TrajectorySession session, TrajectoryParameters parameters)
    {
        session.ApplyParameters(parameters);
        session.TransitionTo(new ReadyState());
    }

    public override void Reset(TrajectorySession session) => session.ResetToDraft();
}
