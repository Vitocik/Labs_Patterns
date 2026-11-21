using TrajectoryMovementLab10.Models;

namespace TrajectoryMovementLab10.State;

/// <summary>Расчёт успешно завершён; можно повторить его или изменить параметры.</summary>
public sealed class CompletedState : TrajectoryState
{
    public override string Name => "Расчёт завершён";

    public override void Configure(TrajectorySession session, TrajectoryParameters parameters)
    {
        session.ApplyParameters(parameters);
        session.TransitionTo(new ReadyState());
    }

    public override void Start(TrajectorySession session) =>
        session.TransitionTo(new CalculatingState());

    public override void Reset(TrajectorySession session) => session.ResetToDraft();
}
