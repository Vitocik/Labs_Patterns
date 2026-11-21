namespace TrajectoryMovementLab9.Models;

/// <summary>Координаты тела в выбранный момент времени.</summary>
public sealed record TrajectoryPoint(double Time, double X, double Y);
