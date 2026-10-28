namespace Lab4_Trajectory_Variant3.Models;

/// <summary>Вычисляет баллистическую траекторию с постоянной поправкой ветра по X.</summary>
public static class TrajectoryCalculator
{
    public static TrajectoryResult Calculate(TrajectoryScenario scenario)
    {
        ArgumentNullException.ThrowIfNull(scenario);
        Validate(scenario);

        double angle = scenario.LaunchAngleDegrees * Math.PI / 180.0;
        double vx = scenario.InitialSpeed * Math.Cos(angle);
        double vy = scenario.InitialSpeed * Math.Sin(angle);
        double discriminant = vy * vy + 2.0 * scenario.Gravity * scenario.StartY;
        double flightTime = (vy + Math.Sqrt(discriminant)) / scenario.Gravity;
        double maxHeight = scenario.StartY + vy * vy / (2.0 * scenario.Gravity);
        double range = (vx + scenario.WindSpeed) * flightTime;

        var points = new List<TrajectoryPoint>();
        for (double t = 0.0; t < flightTime; t += scenario.TimeStep)
        {
            points.Add(CreatePoint(scenario, vx, vy, t));
        }
        points.Add(new TrajectoryPoint(
            flightTime,
            scenario.StartX + range,
            scenario.StartY + vy * flightTime - scenario.Gravity * flightTime * flightTime / 2.0));

        return new TrajectoryResult(scenario, flightTime, maxHeight, range, points);
    }

    private static TrajectoryPoint CreatePoint(
        TrajectoryScenario s, double vx, double vy, double time)
    {
        double x = s.StartX + (vx + s.WindSpeed) * time;
        double y = Math.Max(0.0, s.StartY + vy * time - s.Gravity * time * time / 2.0);
        return new TrajectoryPoint(time, x, y);
    }

    private static void Validate(TrajectoryScenario s)
    {
        if (string.IsNullOrWhiteSpace(s.Name))
            throw new ArgumentException("Название сценария не должно быть пустым.", nameof(s));
        if (!double.IsFinite(s.InitialSpeed) || s.InitialSpeed <= 0)
            throw new ArgumentOutOfRangeException(nameof(s.InitialSpeed), "Скорость должна быть положительной.");
        if (!double.IsFinite(s.LaunchAngleDegrees) || s.LaunchAngleDegrees <= 0 || s.LaunchAngleDegrees >= 90)
            throw new ArgumentOutOfRangeException(nameof(s.LaunchAngleDegrees), "Угол должен находиться между 0° и 90°.");
        if (!double.IsFinite(s.Gravity) || s.Gravity <= 0)
            throw new ArgumentOutOfRangeException(nameof(s.Gravity), "Ускорение свободного падения должно быть положительным.");
        if (!double.IsFinite(s.TimeStep) || s.TimeStep <= 0)
            throw new ArgumentOutOfRangeException(nameof(s.TimeStep), "Шаг расчёта должен быть положительным.");
        if (!double.IsFinite(s.WindSpeed) || !double.IsFinite(s.StartX) || !double.IsFinite(s.StartY) || s.StartY < 0)
            throw new ArgumentOutOfRangeException(nameof(s), "Начальная высота должна быть неотрицательной, параметры — конечными.");
    }
}
