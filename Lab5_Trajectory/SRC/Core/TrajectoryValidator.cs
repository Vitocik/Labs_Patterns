using Lab5_Trajectory_Variant3.Models;

namespace Lab5_Trajectory_Variant3.Core;

/// <summary>Проверяет параметры перед запуском математической модели.</summary>
public static class TrajectoryValidator
{
    public static void Validate(TrajectoryScenario s)
    {
        ArgumentNullException.ThrowIfNull(s);
        if (string.IsNullOrWhiteSpace(s.Name)) throw new ArgumentException("Укажите название сценария.");
        if (!double.IsFinite(s.InitialSpeed) || s.InitialSpeed <= 0) throw new ArgumentOutOfRangeException(nameof(s.InitialSpeed));
        if (!double.IsFinite(s.LaunchAngleDegrees) || s.LaunchAngleDegrees <= 0 || s.LaunchAngleDegrees >= 90) throw new ArgumentOutOfRangeException(nameof(s.LaunchAngleDegrees));
        if (!double.IsFinite(s.Gravity) || s.Gravity <= 0) throw new ArgumentOutOfRangeException(nameof(s.Gravity));
        if (!double.IsFinite(s.TimeStep) || s.TimeStep <= 0) throw new ArgumentOutOfRangeException(nameof(s.TimeStep));
        if (!double.IsFinite(s.WindSpeed) || !double.IsFinite(s.StartX) || !double.IsFinite(s.StartY) || s.StartY < 0) throw new ArgumentOutOfRangeException(nameof(s));
    }
}
