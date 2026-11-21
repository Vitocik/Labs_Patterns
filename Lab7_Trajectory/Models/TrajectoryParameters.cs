namespace Lab7.TrajectoryCommandIterator.Models;

/// <summary>Полный набор исходных параметров одного сценария движения.</summary>
public sealed record TrajectoryParameters(
    string ScenarioName,
    double InitialX,
    double InitialY,
    double InitialSpeed,
    double LaunchAngleDegrees,
    double Gravity,
    double TimeStep,
    double WindSpeed);
