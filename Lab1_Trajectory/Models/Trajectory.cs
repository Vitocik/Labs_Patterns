using TrajectoryMovementLab.Builders;

namespace TrajectoryMovementLab.Models;

/// <summary>
/// Неизменяемый объект, описывающий траекторию движения тела.
/// Создаётся через TrajectoryBuilder.
/// </summary>
public sealed class Trajectory
{
    public double StartX { get; }
    public double StartY { get; }
    public double InitialSpeed { get; }
    public double LaunchAngleDegrees { get; }
    public double Gravity { get; }
    public double TimeStep { get; }

    internal Trajectory(
        double startX,
        double startY,
        double initialSpeed,
        double launchAngleDegrees,
        double gravity,
        double timeStep)
    {
        StartX = startX;
        StartY = startY;
        InitialSpeed = initialSpeed;
        LaunchAngleDegrees = launchAngleDegrees;
        Gravity = gravity;
        TimeStep = timeStep;
    }

    public double GetFlightTime()
    {
        double vy = GetVerticalSpeed();
        return (vy + Math.Sqrt(vy * vy + 2 * Gravity * StartY)) / Gravity;
    }

    public double GetMaximumHeight()
    {
        double vy = GetVerticalSpeed();
        return StartY + vy * vy / (2 * Gravity);
    }

    public double GetRange()
    {
        double vx = GetHorizontalSpeed();
        return StartX + vx * GetFlightTime();
    }

    public IReadOnlyList<TrajectoryPoint> CalculatePoints()
    {
        double angle = ToRadians(LaunchAngleDegrees);
        double vx = InitialSpeed * Math.Cos(angle);
        double vy = InitialSpeed * Math.Sin(angle);
        double flightTime = GetFlightTime();
        var points = new List<TrajectoryPoint>();

        for (double t = 0.0; t < flightTime; t += TimeStep)
        {
            points.Add(CreatePoint(t, vx, vy));
        }

        points.Add(CreatePoint(flightTime, vx, vy));
        return points;
    }

    private TrajectoryPoint CreatePoint(double time, double vx, double vy)
    {
        double x = StartX + vx * time;
        double y = StartY + vy * time - Gravity * time * time / 2.0;
        double verticalVelocity = vy - Gravity * time;

        if (Math.Abs(y) < 1e-10)
        {
            y = 0;
        }

        return new TrajectoryPoint(
            time,
            x,
            Math.Max(0, y),
            verticalVelocity);
    }

    private double GetHorizontalSpeed()
    {
        return InitialSpeed * Math.Cos(ToRadians(LaunchAngleDegrees));
    }

    private double GetVerticalSpeed()
    {
        return InitialSpeed * Math.Sin(ToRadians(LaunchAngleDegrees));
    }

    private static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}
