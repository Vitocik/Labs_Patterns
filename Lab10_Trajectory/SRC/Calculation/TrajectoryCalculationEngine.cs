using TrajectoryMovementLab10.Models;
using TrajectoryMovementLab10.Validation;

namespace TrajectoryMovementLab10.Calculation;

/// <summary>Рассчитывает траекторию по формулам, использованным в лабораторной работе №3.</summary>
public sealed class TrajectoryCalculationEngine
{
    public TrajectoryResult Calculate(TrajectoryParameters parameters)
    {
        TrajectoryValidator.Validate(parameters);

        double angle = parameters.LaunchAngleDeg * Math.PI / 180.0;
        double velocityX = parameters.InitialSpeed * Math.Cos(angle);
        double velocityY = parameters.InitialSpeed * Math.Sin(angle);
        double discriminant = velocityY * velocityY +
                              2.0 * parameters.Gravity * parameters.StartY;
        double flightTime = (velocityY + Math.Sqrt(discriminant)) / parameters.Gravity;
        double maximumHeight = parameters.StartY +
                               velocityY * velocityY / (2.0 * parameters.Gravity);
        double range = parameters.StartX +
                       (velocityX + parameters.WindSpeed) * flightTime;

        var points = new List<TrajectoryPoint>();
        for (double time = 0.0; time < flightTime; time += parameters.TimeStep)
        {
            points.Add(CreatePoint(parameters, time, angle));
        }
        points.Add(CreatePoint(parameters, flightTime, angle));

        return new TrajectoryResult(
            "Баллистическая модель с постоянным ветром",
            flightTime,
            maximumHeight,
            range,
            points.ToArray());
    }

    private static TrajectoryPoint CreatePoint(
        TrajectoryParameters parameters,
        double time,
        double angle)
    {
        double velocityX = parameters.InitialSpeed * Math.Cos(angle);
        double velocityY = parameters.InitialSpeed * Math.Sin(angle);
        double x = parameters.StartX + (velocityX + parameters.WindSpeed) * time;
        double y = parameters.StartY + velocityY * time -
                   parameters.Gravity * time * time / 2.0;

        return new TrajectoryPoint(time, x, Math.Max(0.0, y));
    }
}
