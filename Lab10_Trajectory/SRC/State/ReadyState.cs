using TrajectoryMovementLab10.Models;

namespace TrajectoryMovementLab10.State;

/// <summary>Параметры заданы и могут быть рассчитаны или скорректированы.</summary>
public sealed class ReadyState : TrajectoryState
{
    public override string Name => "Готов к расчёту";

    public override void Configure(TrajectorySession session, TrajectoryParameters parameters) =>
        session.ApplyParameters(parameters);

    public override void Start(TrajectorySession session) =>
        session.TransitionTo(new CalculatingState());

    public override void Reset(TrajectorySession session) => session.ResetToDraft();
}
