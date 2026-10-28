namespace TrajectoryMovementLab.Models;

/// <summary>Результат расчёта, общий для всех моделей движения.</summary>
public sealed record TrajectoryResult(
    string ModelName,
    double FlightTime,
    double MaximumHeight,
    double Range,
    IReadOnlyList<TrajectoryPoint> Points);
