using Lab8.TrajectoryChainOfResponsibility.Models;

namespace Lab8.TrajectoryChainOfResponsibility.Validation;

/// <summary>Третий этап: шаг дискретизации и допустимый размер траектории.</summary>
public sealed class TrajectorySamplingHandler : TrajectoryValidationHandler
{
    public const int MaximumTrajectoryPoints = 1_000_000;
    protected override string Name => nameof(TrajectorySamplingHandler);

    protected override string? ValidateCurrent(TrajectoryParameters? p)
    {
        if (p is null)
            return "Параметры сценария не переданы.";
        if (p.TimeStep <= 0)
            return "Шаг расчёта должен быть положительным.";

        var angle = p.LaunchAngleDegrees * Math.PI / 180.0;
        var velocityY = p.InitialSpeed * Math.Sin(angle);
        var flightTime = (velocityY + Math.Sqrt(
            velocityY * velocityY + 2.0 * p.Gravity * p.InitialY)) / p.Gravity;
        var estimatedCount = Math.Ceiling(flightTime / p.TimeStep) + 1.0;

        if (!double.IsFinite(estimatedCount) || estimatedCount > MaximumTrajectoryPoints)
            return $"Ожидается слишком много точек траектории; предел — {MaximumTrajectoryPoints:N0}. Увеличьте шаг расчёта.";
        return null;
    }
}
