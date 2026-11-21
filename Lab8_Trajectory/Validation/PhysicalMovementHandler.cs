using Lab8.TrajectoryChainOfResponsibility.Models;

namespace Lab8.TrajectoryChainOfResponsibility.Validation;

/// <summary>Второй этап: физические ограничения движения тела.</summary>
public sealed class PhysicalMovementHandler : TrajectoryValidationHandler
{
    protected override string Name => nameof(PhysicalMovementHandler);

    protected override string? ValidateCurrent(TrajectoryParameters? p)
    {
        if (p is null)
            return "Параметры сценария не переданы.";
        if (p.InitialY < 0)
            return "Начальная высота не может быть отрицательной.";
        if (p.InitialSpeed <= 0)
            return "Начальная скорость должна быть положительной.";
        if (p.LaunchAngleDegrees <= 0 || p.LaunchAngleDegrees >= 90)
            return "Угол запуска должен находиться строго между 0° и 90°.";
        if (p.Gravity <= 0)
            return "Ускорение свободного падения должно быть положительным.";
        return null;
    }
}
