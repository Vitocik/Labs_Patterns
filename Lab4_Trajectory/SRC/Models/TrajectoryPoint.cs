namespace Lab4_Trajectory_Variant3.Models;

/// <summary>Координаты тела в определённый момент времени.</summary>
public sealed record TrajectoryPoint(double TimeSeconds, double X, double Y);
