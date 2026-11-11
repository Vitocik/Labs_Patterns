using Lab6.TrajectoryProxy.Models;

namespace Lab6.TrajectoryProxy.Core;

/// <summary>Общий контракт реального сервиса и заместителя (Subject).</summary>
public interface ITrajectoryService
{
    TrajectoryResult Calculate(TrajectoryParameters parameters);
}
