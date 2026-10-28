using TrajectoryMovementLab.Models;
using TrajectoryMovementLab.Validation;

namespace TrajectoryMovementLab.Bridge;

/// <summary>Общие формулы баллистического движения.</summary>
internal static class TrajectoryMath
{
    public static TrajectoryResult Calculate(
        TrajectoryParameters p,
        string modelName,
        double horizontalWind)
    {
        TrajectoryValidator.Validate(p);

        double angle = p.LaunchAngleDeg * Math.PI / 180.0;
        double vx = p.InitialSpeed * Math.Cos(angle);
        double vy = p.InitialSpeed * Math.Sin(angle);
        double discriminant = vy * vy + 2.0 * p.Gravity * p.StartY;
        double flightTime = (vy + Math.Sqrt(discriminant)) / p.Gravity;
        double maximumHeight = p.StartY + vy * vy / (2.0 * p.Gravity);
        double range = p.StartX + (vx + horizontalWind) * flightTime;

        var points = new List<TrajectoryPoint>();
        for (double time = 0.0; time < flightTime; time += p.TimeStep)
        {
            points.Add(CreatePoint(p, time, angle, horizontalWind));
        }
        points.Add(CreatePoint(p, flightTime, angle, horizontalWind));

        return new TrajectoryResult(
            modelName,
            flightTime,
            maximumHeight,
            range,
            points);
    }

    private static TrajectoryPoint CreatePoint(
        TrajectoryParameters p,
        double time,
        double angle,
        double horizontalWind)
    {
        double vx = p.InitialSpeed * Math.Cos(angle);
        double vy = p.InitialSpeed * Math.Sin(angle);
        double x = p.StartX + (vx + horizontalWind) * time;
        double y = p.StartY + vy * time - p.Gravity * time * time / 2.0;
        return new TrajectoryPoint(time, x, Math.Max(0.0, y));
    }
}
