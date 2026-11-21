using Lab8.TrajectoryChainOfResponsibility.Models;

namespace Lab8.TrajectoryChainOfResponsibility.Core;

/// <summary>Математическое ядро; вызывается после успешного прохождения цепочки.</summary>
internal sealed class RealTrajectoryService : ITrajectoryService
{
    public TrajectoryResult Calculate(TrajectoryParameters p)
    {
        var angle = p.LaunchAngleDegrees * Math.PI / 180.0;
        var velocityX = p.InitialSpeed * Math.Cos(angle);
        var velocityY = p.InitialSpeed * Math.Sin(angle);
        var flightTime = (velocityY + Math.Sqrt(
            velocityY * velocityY + 2.0 * p.Gravity * p.InitialY)) / p.Gravity;
        var maxHeight = p.InitialY + velocityY * velocityY / (2.0 * p.Gravity);
        var horizontalVelocity = velocityX + p.WindSpeed;
        var landingX = p.InitialX + horizontalVelocity * flightTime;

        var points = new List<TrajectoryPoint>();
        for (var index = 0; ; index++)
        {
            var time = index * p.TimeStep;
            if (time >= flightTime)
                break;
            points.Add(CreatePoint(time));
        }
        points.Add(CreatePoint(flightTime));

        return new TrajectoryResult(p.ScenarioName, flightTime, maxHeight, landingX, points);

        TrajectoryPoint CreatePoint(double time)
        {
            var x = p.InitialX + horizontalVelocity * time;
            var y = p.InitialY + velocityY * time - p.Gravity * time * time / 2.0;
            return new TrajectoryPoint(time, x, Math.Max(0.0, y));
        }
    }
}
