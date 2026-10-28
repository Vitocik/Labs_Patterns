namespace TrajectoryMovementLab.Models;

public sealed class TrajectoryResult
{
    public TrajectoryResult(
        double flightTime,
        double maximumHeight,
        double range,
        IReadOnlyList<TrajectoryPoint> points)
    {
        FlightTime = flightTime;
        MaximumHeight = maximumHeight;
        Range = range;
        Points = points;
    }

    public double FlightTime { get; }
    public double MaximumHeight { get; }
    public double Range { get; }
    public IReadOnlyList<TrajectoryPoint> Points { get; }
}
