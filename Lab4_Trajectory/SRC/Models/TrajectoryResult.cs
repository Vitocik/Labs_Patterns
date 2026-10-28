namespace Lab4_Trajectory_Variant3.Models;

/// <summary>Итог расчёта одной траектории.</summary>
public sealed record TrajectoryResult(
    TrajectoryScenario Scenario,
    double FlightTimeSeconds,
    double MaximumHeight,
    double RangeMeters,
    IReadOnlyList<TrajectoryPoint> Points);
