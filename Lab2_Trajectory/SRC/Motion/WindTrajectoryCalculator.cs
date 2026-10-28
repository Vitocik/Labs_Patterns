using TrajectoryMovementLab.Models;

namespace TrajectoryMovementLab.Motion;

/// <summary>Упрощённая модель: постоянный попутный/встречный ветер меняет X-скорость.</summary>
public sealed class WindTrajectoryCalculator : ITrajectoryCalculator
{
    public TrajectoryResult Calculate(TrajectoryScenario scenario)
    {
        Validate(scenario);
        var angle = scenario.LaunchAngleDeg * Math.PI / 180.0;
        var vx = scenario.InitialSpeed * Math.Cos(angle) + scenario.WindSpeed;
        var vy = scenario.InitialSpeed * Math.Sin(angle);
        var tFlight = (vy + Math.Sqrt(vy * vy + 2 * scenario.Gravity * scenario.Y0)) / scenario.Gravity;
        var tPeak = vy / scenario.Gravity;
        var maxHeight = scenario.Y0 + vy * tPeak - scenario.Gravity * tPeak * tPeak / 2;
        var range = scenario.X0 + vx * tFlight;
        var points = new List<TrajectoryPoint>();
        for (var t = 0.0; t < tFlight; t += scenario.TimeStep)
            points.Add(new TrajectoryPoint(
                t,
                scenario.X0 + vx * t,
                scenario.Y0 + vy * t - scenario.Gravity * t * t / 2));
        points.Add(new TrajectoryPoint(tFlight, range, 0));
        return new TrajectoryResult(tFlight, maxHeight, range, points);
    }

    private static void Validate(TrajectoryScenario s)
    {
        if (s.InitialSpeed <= 0 || s.Gravity <= 0 || s.TimeStep <= 0)
            throw new ArgumentException("Скорость, g и шаг времени должны быть положительными.");
        if (s.LaunchAngleDeg <= 0 || s.LaunchAngleDeg >= 90)
            throw new ArgumentException("Угол запуска должен находиться в диапазоне (0; 90) градусов.");
        if (s.Y0 < 0)
            throw new ArgumentException("Начальная высота не может быть отрицательной.");
    }
}
