using TrajectoryMovementLab9.Models;

namespace TrajectoryMovementLab9.Mediator;

/// <summary>Контракт посредника, через который коллеги обмениваются запросами.</summary>
public interface ITrajectoryMediator
{
    void ParametersSubmitted(TrajectoryParameters parameters);
    void CalculationRequested();
    void SnapshotSaveRequested();
    void SnapshotRestoreRequested(int number);
    void SnapshotListRequested();
}
