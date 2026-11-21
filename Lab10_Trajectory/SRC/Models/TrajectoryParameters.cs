namespace TrajectoryMovementLab10.Models;

/// <summary>Исходные параметры движения тела, брошенного под углом к горизонту.</summary>
public sealed record TrajectoryParameters(
    string Name,
    double StartX,
    double StartY,
    double InitialSpeed,
    double LaunchAngleDeg,
    double Gravity,
    double TimeStep,
    double WindSpeed = 0.0);
