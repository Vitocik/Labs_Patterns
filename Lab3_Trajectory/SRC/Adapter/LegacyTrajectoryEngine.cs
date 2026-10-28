namespace TrajectoryMovementLab.Adapter;

/// <summary>Формат точки старого компонента, несовместимый с доменной моделью.</summary>
public sealed record LegacyPoint(double SecondsElapsed, double XCoordinate, double HeightMeters);

/// <summary>Результат старого компонента с историческими названиями свойств.</summary>
public sealed record LegacyFlightData(
    double FlightSeconds,
    double HighestPointY,
    double LandingCoordinateX,
    IReadOnlyList<LegacyPoint> Samples);

/// <summary>
/// Интерфейс прежней библиотеки: угол ожидается в радианах, а результат имеет другой формат.
/// </summary>
public interface ILegacyTrajectoryEngine
{
    LegacyFlightData Solve(
        double startX,
        double startY,
        double speed,
        double angleRadians,
        double gravity,
        double horizontalWind,
        double samplePeriod);
}

/// <summary>Условный старый компонент, который нельзя напрямую вызвать как IFlightModel.</summary>
public sealed class LegacyTrajectoryEngine : ILegacyTrajectoryEngine
{
    public LegacyFlightData Solve(
        double startX,
        double startY,
        double speed,
        double angleRadians,
        double gravity,
        double horizontalWind,
        double samplePeriod)
    {
        if (speed <= 0 || gravity <= 0 || samplePeriod <= 0)
            throw new ArgumentOutOfRangeException(nameof(speed),
                "Скорость, ускорение и интервал должны быть положительными.");

        double vx = speed * Math.Cos(angleRadians);
        double vy = speed * Math.Sin(angleRadians);
        double duration = (vy + Math.Sqrt(vy * vy + 2.0 * gravity * startY)) / gravity;
        double apex = startY + vy * vy / (2.0 * gravity);
        double landingX = startX + (vx + horizontalWind) * duration;
        var samples = new List<LegacyPoint>();

        for (double t = 0.0; t < duration; t += samplePeriod)
        {
            samples.Add(CreateLegacyPoint(startX, startY, vx, vy,
                gravity, horizontalWind, t));
        }
        samples.Add(CreateLegacyPoint(startX, startY, vx, vy,
            gravity, horizontalWind, duration));

        return new LegacyFlightData(duration, apex, landingX, samples);
    }

    private static LegacyPoint CreateLegacyPoint(
        double startX,
        double startY,
        double vx,
        double vy,
        double gravity,
        double horizontalWind,
        double time)
    {
        double x = startX + (vx + horizontalWind) * time;
        double y = startY + vy * time - gravity * time * time / 2.0;
        return new LegacyPoint(time, x, Math.Max(0.0, y));
    }
}
