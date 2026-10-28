using TrajectoryMovementLab.Models;

namespace TrajectoryMovementLab.Presentation;

public sealed class WindTrajectoryFormatter : ITrajectoryFormatter
{
    public string Format(TrajectoryScenario s, TrajectoryResult r)
    {
        return $"Параметры: v0={s.InitialSpeed:F1} м/с, α={s.LaunchAngleDeg:F1}°, " +
               $"ветер={s.WindSpeed:+0.0;-0.0;0.0} м/с\n" +
               $"Результат: T={r.FlightTime:F3} с, Hmax={r.MaximumHeight:F3} м, L={r.Range:F3} м";
    }
}
