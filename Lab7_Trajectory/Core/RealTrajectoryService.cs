using Lab7.TrajectoryCommandIterator.Models;

namespace Lab7.TrajectoryCommandIterator.Core;

/// <summary>Выполняет математический расчёт без знания о командах и итераторах.</summary>
public sealed class RealTrajectoryService : ITrajectoryService
{
    public TrajectoryResult Calculate(TrajectoryParameters parameters)
    {
        TrajectoryInputValidator.Validate(parameters);

        var angle = parameters.LaunchAngleDegrees * Math.PI / 180.0;
        var velocityX = parameters.InitialSpeed * Math.Cos(angle);
        var velocityY = parameters.InitialSpeed * Math.Sin(angle);
        var flightTime = (velocityY + Math.Sqrt(
            velocityY * velocityY + 2.0 * parameters.Gravity * parameters.InitialY))
            / parameters.Gravity;
        var maxHeight = parameters.InitialY
            + velocityY * velocityY / (2.0 * parameters.Gravity);
        var horizontalVelocity = velocityX + parameters.WindSpeed;
        var landingX = parameters.InitialX + horizontalVelocity * flightTime;

        var estimatedCount = Math.Ceiling(flightTime / parameters.TimeStep) + 1.0;
        if (!double.IsFinite(estimatedCount)
            || estimatedCount > TrajectoryInputValidator.MaximumTrajectoryPoints)
        {
            throw new ArgumentOutOfRangeException(
                nameof(parameters.TimeStep),
                $"Количество точек превышает допустимый предел {TrajectoryInputValidator.MaximumTrajectoryPoints:N0}.");
        }

        var points = new List<TrajectoryPoint>((int)estimatedCount + 1);
        for (var index = 0; ; index++)
        {
            var time = index * parameters.TimeStep;
            if (time >= flightTime)
                break;
            points.Add(CreatePoint(time));
        }
        points.Add(CreatePoint(flightTime));

        return new TrajectoryResult(
            parameters.ScenarioName,
            flightTime,
            maxHeight,
            landingX,
            points);

        TrajectoryPoint CreatePoint(double time)
        {
            var x = parameters.InitialX + horizontalVelocity * time;
            var y = parameters.InitialY + velocityY * time
                - parameters.Gravity * time * time / 2.0;
            return new TrajectoryPoint(time, x, Math.Max(0.0, y));
        }
    }
}
