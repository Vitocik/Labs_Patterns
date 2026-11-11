namespace Lab6.TrajectoryProxy.Models;

/// <summary>Итоговые характеристики и дискретные точки траектории.</summary>
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
        Points = points;
    }

    public string ScenarioName { get; }
    public double FlightTimeSeconds { get; }
    public double MaxHeightMeters { get; }

    /// <summary>Горизонтальная координата точки приземления.</summary>
    public double LandingX { get; }

    /// <summary>
    /// Дальность в принятой в отчёте системе координат:
    /// L = x0 + (vx + w) * T. При x0 = 0 совпадает с длиной полёта по X.
    /// </summary>
    public double RangeMeters => LandingX;

    public IReadOnlyList<TrajectoryPoint> Points { get; }
}
