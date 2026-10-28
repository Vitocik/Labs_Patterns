namespace Lab4_Trajectory_Variant3.Models;

/// <summary>Параметры отдельного сценария движения.</summary>
public sealed record TrajectoryScenario(
    string Name,
    double InitialSpeed,
    double LaunchAngleDegrees,
    double Gravity = 9.81,
    double TimeStep = 0.5,
    double WindSpeed = 0.0,
    double StartX = 0.0,
    double StartY = 0.0);
