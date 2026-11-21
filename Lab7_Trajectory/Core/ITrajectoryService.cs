using Lab7.TrajectoryCommandIterator.Models;

namespace Lab7.TrajectoryCommandIterator.Core;

/// <summary>Контракт расчётной подсистемы.</summary>
public interface ITrajectoryService
{
    TrajectoryResult Calculate(TrajectoryParameters parameters);
}
