using TrajectoryMovementLab.Models;

namespace TrajectoryMovementLab.Motion;

public interface ITrajectoryCalculator
{
    TrajectoryResult Calculate(TrajectoryScenario scenario);
}
