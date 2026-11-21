namespace TrajectoryMovementLab9.Models;

/// <summary>Итоговые характеристики и дискретные точки рассчитанной траектории.</summary>
public sealed record TrajectoryResult(
    string ModelName,
    double FlightTime,
    double MaximumHeight,
    double Range,
    IReadOnlyList<TrajectoryPoint> Points);
