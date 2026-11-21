namespace TrajectoryMovementLab9.Mediator;

/// <summary>Коллега, отправляющий посреднику команды управления снимками.</summary>
public sealed class TrajectoryHistoryPanel(ITrajectoryMediator mediator)
{
    public void SaveSnapshot() => mediator.SnapshotSaveRequested();
    public void RestoreSnapshot(int number) => mediator.SnapshotRestoreRequested(number);
    public void ShowHistory() => mediator.SnapshotListRequested();
}
