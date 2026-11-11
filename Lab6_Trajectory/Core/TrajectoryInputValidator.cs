using Lab6.TrajectoryProxy.Models;

namespace Lab6.TrajectoryProxy.Core;

/// <summary>Централизованная проверка исходных данных сценария.</summary>
internal static class TrajectoryInputValidator
{
    public static void Validate(TrajectoryParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        if (string.IsNullOrWhiteSpace(parameters.ScenarioName))
            throw new ArgumentException("Название сценария должно быть задано.", nameof(parameters));

        EnsureFinite(parameters.InitialX, nameof(parameters.InitialX));
        EnsureFinite(parameters.InitialY, nameof(parameters.InitialY));
        EnsureFinite(parameters.InitialSpeed, nameof(parameters.InitialSpeed));
        EnsureFinite(parameters.LaunchAngleDegrees, nameof(parameters.LaunchAngleDegrees));
        EnsureFinite(parameters.Gravity, nameof(parameters.Gravity));
        EnsureFinite(parameters.TimeStep, nameof(parameters.TimeStep));
        EnsureFinite(parameters.WindSpeed, nameof(parameters.WindSpeed));

        if (parameters.InitialY < 0)
            throw new ArgumentOutOfRangeException(nameof(parameters.InitialY), "Начальная высота не может быть отрицательной.");
        if (parameters.InitialSpeed <= 0)
            throw new ArgumentOutOfRangeException(nameof(parameters.InitialSpeed), "Начальная скорость должна быть положительной.");
        if (parameters.LaunchAngleDegrees <= 0 || parameters.LaunchAngleDegrees >= 90)
            throw new ArgumentOutOfRangeException(nameof(parameters.LaunchAngleDegrees), "Угол запуска должен находиться строго между 0° и 90°.");
        if (parameters.Gravity <= 0)
            throw new ArgumentOutOfRangeException(nameof(parameters.Gravity), "Ускорение свободного падения должно быть положительным.");
        if (parameters.TimeStep <= 0)
            throw new ArgumentOutOfRangeException(nameof(parameters.TimeStep), "Шаг расчёта должен быть положительным.");
    }

    private static void EnsureFinite(double value, string parameterName)
    {
        if (!double.IsFinite(value))
            throw new ArgumentOutOfRangeException(parameterName, "Параметр должен быть конечным числом.");
    }
}
