using Lab5_Trajectory_Variant3.Models;

namespace Lab5_Trajectory_Variant3.Core;

/// <summary>Расчёт полёта без сопротивления воздуха.</summary>
public sealed class TrajectoryCalculator
{
    public TrajectoryResult Calculate(TrajectoryScenario s)
    {
        TrajectoryValidator.Validate(s);
        double a = s.LaunchAngleDegrees * Math.PI / 180.0;
        double vx = s.InitialSpeed * Math.Cos(a);
        double vy = s.InitialSpeed * Math.Sin(a);
        double flight = (vy + Math.Sqrt(vy * vy + 2 * s.Gravity * s.StartY)) / s.Gravity;
        double height = s.StartY + vy * vy / (2 * s.Gravity);
        double range = (vx + s.WindSpeed) * flight;
        var points = new List<TrajectoryPoint>();
        for (double t = 0; t < flight; t += s.TimeStep)
        {
            points.Add(new TrajectoryPoint(t, s.StartX + (vx + s.WindSpeed) * t,
                Math.Max(0, s.StartY + vy * t - s.Gravity * t * t / 2)));
        }
        points.Add(new TrajectoryPoint(flight, s.StartX + range, 0));
        return new TrajectoryResult(s, flight, height, range, points);
    }
}
