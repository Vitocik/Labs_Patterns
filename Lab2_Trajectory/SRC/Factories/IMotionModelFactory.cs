using TrajectoryMovementLab.Motion;
using TrajectoryMovementLab.Presentation;

namespace TrajectoryMovementLab.Factories;

/// <summary>Abstract Factory для взаимосвязанного семейства объектов расчёта.</summary>
public interface IMotionModelFactory
{
    ITrajectoryCalculator CreateCalculator();
    ITrajectoryFormatter CreateFormatter();
}
