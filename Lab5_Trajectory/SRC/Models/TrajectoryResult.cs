namespace Lab5_Trajectory_Variant3.Models;

public sealed record TrajectoryResult(
    TrajectoryScenario Scenario, double FlightTimeSeconds, double MaximumHeight,
    double RangeMeters, IReadOnlyList<TrajectoryPoint> Points);
