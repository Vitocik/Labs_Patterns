namespace Lab8.TrajectoryChainOfResponsibility.Models;

/// <summary>Координаты тела в заданный момент времени.</summary>
public sealed record TrajectoryPoint(double TimeSeconds, double X, double Y);
