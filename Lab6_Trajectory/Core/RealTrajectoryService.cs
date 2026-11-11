using Lab6.TrajectoryProxy.Models;

namespace Lab6.TrajectoryProxy.Core;

/// <summary>
/// Реальный объект (RealSubject): проверяет данные и непосредственно
/// вычисляет характеристики полёта и координаты точек траектории.
/// </summary>
public sealed class RealTrajectoryService : ITrajectoryService
{
    private const int MaximumTrajectoryPoints = 1_000_000;

    public TrajectoryResult Calculate(TrajectoryParameters parameters)
    {
        TrajectoryInputValidator.Validate(parameters);

        var angleRadians = parameters.LaunchAngleDegrees * Math.PI / 180.0;
        var velocityX = parameters.InitialSpeed * Math.Cos(angleRadians);
        var velocityY = parameters.InitialSpeed * Math.Sin(angleRadians);

        // Положительный корень уравнения y(t) = 0.
        var flightTime = (velocityY + Math.Sqrt(
            velocityY * velocityY + 2.0 * parameters.Gravity * parameters.InitialY))
            / parameters.Gravity;

        var maxHeight = parameters.InitialY
            + velocityY * velocityY / (2.0 * parameters.Gravity);

        var horizontalVelocity = velocityX + parameters.WindSpeed;
        var landingX = parameters.InitialX + horizontalVelocity * flightTime;

        var estimatedPointCount = Math.Ceiling(flightTime / parameters.TimeStep) + 1.0;
        if (!double.IsFinite(estimatedPointCount) || estimatedPointCount > MaximumTrajectoryPoints)
        {
            throw new ArgumentOutOfRangeException(
                nameof(parameters.TimeStep),
                $"Получится слишком много точек. Уменьшите длительность или увеличьте шаг (лимит: {MaximumTrajectoryPoints:N0}).");
        }

        var points = new List<TrajectoryPoint>((int)estimatedPointCount + 1);
        for (var index = 0; ; index++)
        {
            var time = index * parameters.TimeStep;
            if (time >= flightTime)
                break;

            points.Add(CreatePoint(time));
        }

        // Последняя точка всегда соответствует точному моменту приземления.
        points.Add(CreatePoint(flightTime));

        return new TrajectoryResult(
            parameters.ScenarioName,
            flightTime,
            maxHeight,
            landingX,
            points.AsReadOnly());

        TrajectoryPoint CreatePoint(double time)
        {
            var x = parameters.InitialX + horizontalVelocity * time;
            var y = parameters.InitialY + velocityY * time
                - parameters.Gravity * time * time / 2.0;
            return new TrajectoryPoint(time, x, Math.Max(0.0, y));
        }
    }
}
