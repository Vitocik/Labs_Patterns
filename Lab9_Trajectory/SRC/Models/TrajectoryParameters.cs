namespace TrajectoryMovementLab9.Models;

/// <summary>Исходные параметры запуска тела, брошенного под углом к горизонту.</summary>
public sealed record TrajectoryParameters(
    string Name,
    double StartX,
    double StartY,
    double InitialSpeed,
    double LaunchAngleDeg,
    double Gravity,
    double TimeStep,
    double WindSpeed = 0.0);
