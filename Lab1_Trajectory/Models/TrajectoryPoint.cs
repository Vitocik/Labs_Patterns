namespace TrajectoryMovementLab.Models;

/// <summary>
/// Состояние движущегося тела в заданный момент времени.
/// </summary>
public sealed record TrajectoryPoint(
    double Time,
    double X,
    double Y,
    double VerticalVelocity);
