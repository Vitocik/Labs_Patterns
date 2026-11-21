using TrajectoryMovementLab10.Models;

namespace TrajectoryMovementLab10.Validation;

/// <summary>Проверяет исходные данные расчёта.</summary>
public static class TrajectoryValidator
{
    public static void Validate(TrajectoryParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        if (string.IsNullOrWhiteSpace(parameters.Name))
            throw new ArgumentException("Не задано название сценария.", nameof(parameters));
        if (!double.IsFinite(parameters.StartX) || !double.IsFinite(parameters.StartY))
            throw new ArgumentException("Координаты старта должны быть конечными числами.", nameof(parameters));
        if (parameters.StartY < 0)
            throw new ArgumentOutOfRangeException(nameof(parameters), "Начальная высота не может быть отрицательной.");
        if (!double.IsFinite(parameters.InitialSpeed) || parameters.InitialSpeed <= 0)
            throw new ArgumentOutOfRangeException(nameof(parameters), "Начальная скорость должна быть положительной.");
        if (!double.IsFinite(parameters.LaunchAngleDeg) || parameters.LaunchAngleDeg <= 0 || parameters.LaunchAngleDeg >= 90)
            throw new ArgumentOutOfRangeException(nameof(parameters), "Угол запуска должен находиться между 0° и 90°.");
        if (!double.IsFinite(parameters.Gravity) || parameters.Gravity <= 0)
            throw new ArgumentOutOfRangeException(nameof(parameters), "Ускорение свободного падения должно быть положительным.");
        if (!double.IsFinite(parameters.TimeStep) || parameters.TimeStep <= 0)
            throw new ArgumentOutOfRangeException(nameof(parameters), "Шаг расчёта должен быть положительным.");
        if (!double.IsFinite(parameters.WindSpeed))
            throw new ArgumentOutOfRangeException(nameof(parameters), "Скорость ветра должна быть конечным числом.");
    }
}
