using TrajectoryMovementLab.Models;

namespace TrajectoryMovementLab.Validation;

/// <summary>Единая проверка входных параметров перед физическим расчётом.</summary>
public static class TrajectoryValidator
{
    public static void Validate(TrajectoryParameters p)
    {
        ArgumentNullException.ThrowIfNull(p);

        if (string.IsNullOrWhiteSpace(p.Name))
            throw new ArgumentException("Не задано название сценария.", nameof(p));
        if (!double.IsFinite(p.StartX) || !double.IsFinite(p.StartY))
            throw new ArgumentException("Координаты старта должны быть конечными числами.", nameof(p));
        if (p.StartY < 0)
            throw new ArgumentOutOfRangeException(nameof(p), "Начальная высота не может быть отрицательной.");
        if (!double.IsFinite(p.InitialSpeed) || p.InitialSpeed <= 0)
            throw new ArgumentOutOfRangeException(nameof(p), "Начальная скорость должна быть положительной.");
        if (!double.IsFinite(p.LaunchAngleDeg) || p.LaunchAngleDeg <= 0 || p.LaunchAngleDeg >= 90)
            throw new ArgumentOutOfRangeException(nameof(p), "Угол запуска должен быть больше 0° и меньше 90°.");
        if (!double.IsFinite(p.Gravity) || p.Gravity <= 0)
            throw new ArgumentOutOfRangeException(nameof(p), "Ускорение свободного падения должно быть положительным.");
        if (!double.IsFinite(p.TimeStep) || p.TimeStep <= 0)
            throw new ArgumentOutOfRangeException(nameof(p), "Шаг расчёта должен быть положительным.");
        if (!double.IsFinite(p.WindSpeed))
            throw new ArgumentOutOfRangeException(nameof(p), "Скорость ветра должна быть конечным числом.");
    }
}
