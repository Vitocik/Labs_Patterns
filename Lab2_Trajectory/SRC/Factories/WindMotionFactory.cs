using TrajectoryMovementLab.Motion;
using TrajectoryMovementLab.Presentation;

namespace TrajectoryMovementLab.Factories;

public sealed class WindMotionFactory : IMotionModelFactory
{
    public ITrajectoryCalculator CreateCalculator() => new WindTrajectoryCalculator();
    public ITrajectoryFormatter CreateFormatter() => new WindTrajectoryFormatter();
}
