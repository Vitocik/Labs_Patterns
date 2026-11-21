using Lab8.TrajectoryChainOfResponsibility.Models;

namespace Lab8.TrajectoryChainOfResponsibility.Core;

/// <summary>Контракт сервиса расчёта траектории.</summary>
public interface ITrajectoryService
{
    TrajectoryResult Calculate(TrajectoryParameters parameters);
}
