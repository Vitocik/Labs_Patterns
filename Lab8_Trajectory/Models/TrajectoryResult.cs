using System.Collections.ObjectModel;

namespace Lab8.TrajectoryChainOfResponsibility.Models;

/// <summary>Итоговые характеристики и дискретные точки рассчитанной траектории.</summary>
public sealed class TrajectoryResult
{
    public TrajectoryResult(
        string scenarioName,
        double flightTimeSeconds,
        double maxHeightMeters,
        double landingX,
        IReadOnlyList<TrajectoryPoint> points)
    {
        ScenarioName = scenarioName;
        FlightTimeSeconds = flightTimeSeconds;
        MaxHeightMeters = maxHeightMeters;
        LandingX = landingX;
        Points = new ReadOnlyCollection<TrajectoryPoint>(points.ToList());
    }

    public string ScenarioName { get; }
    public double FlightTimeSeconds { get; }
    public double MaxHeightMeters { get; }
    public double LandingX { get; }
    public double RangeMeters => LandingX;
    public IReadOnlyList<TrajectoryPoint> Points { get; }
}
