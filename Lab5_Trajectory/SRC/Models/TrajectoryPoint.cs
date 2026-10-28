namespace Lab5_Trajectory_Variant3.Models;

/// <summary>Изменяемая для каждого момента времени (внешняя) часть данных.</summary>
public sealed record TrajectoryPoint(double TimeSeconds, double X, double Y);
