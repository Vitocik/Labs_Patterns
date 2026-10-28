using TrajectoryMovementLab.Models;

namespace TrajectoryMovementLab.Presentation;

public interface ITrajectoryFormatter
{
    string Format(TrajectoryScenario scenario, TrajectoryResult result);
}
