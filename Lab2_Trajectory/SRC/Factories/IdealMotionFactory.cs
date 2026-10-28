using TrajectoryMovementLab.Motion;
using TrajectoryMovementLab.Presentation;

namespace TrajectoryMovementLab.Factories;

public sealed class IdealMotionFactory : IMotionModelFactory
{
    public ITrajectoryCalculator CreateCalculator() => new IdealTrajectoryCalculator();
    public ITrajectoryFormatter CreateFormatter() => new IdealTrajectoryFormatter();
}
