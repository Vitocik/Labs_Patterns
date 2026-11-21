namespace TrajectoryMovementLab10.Models;

/// <summary>Основные характеристики полёта и набор координатных точек.</summary>
public sealed record TrajectoryResult(
    string ModelName,
    double FlightTime,
    double MaximumHeight,
    double Range,
    IReadOnlyList<TrajectoryPoint> Points);
