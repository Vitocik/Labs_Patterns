using System.Globalization;
using TrajectoryMovementLab.Builders;
using TrajectoryMovementLab.Directors;
using TrajectoryMovementLab.Exceptions;
using TrajectoryMovementLab.Models;
using TrajectoryMovementLab.Patterns;

Console.OutputEncoding = System.Text.Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

PrintHeader();

SimulationSettings first = SimulationSettings.Instance;
SimulationSettings second = SimulationSettings.Instance;

Console.WriteLine("[1] Проверка паттерна «Одиночка»");
Console.WriteLine($"  Один и тот же объект настроек: {ReferenceEquals(first, second)}");
Console.WriteLine($"  g по умолчанию: {first.DefaultGravity:F2} м/с²");
Console.WriteLine($"  v0 по умолчанию: {first.DefaultInitialSpeed:F2} м/с");
Console.WriteLine($"  угол по умолчанию: {first.DefaultLaunchAngle:F2}°");
Console.WriteLine($"  шаг по умолчанию: {first.DefaultTimeStep:F2} с");

try
{
    Console.WriteLine("\n[2] Пошаговая сборка траектории через «Строитель»");

    Trajectory trajectory = new TrajectoryBuilder()
        .SetStartPosition(0, 0)
        .SetInitialSpeed(20)
        .SetLaunchAngle(45)
        .SetGravity(9.81)
        .SetTimeStep(0.2)
        .Build();

    PrintParameters(trajectory);
    PrintStatistics(trajectory);
    PrintPoints(trajectory.CalculatePoints());

    Console.WriteLine("\n[3] Проверка «Распорядителя»");
    var director = new TrajectoryScenarioDirector(new TrajectoryBuilder());
    Trajectory presetTrajectory = director.Construct(TrajectoryPreset.FromPlatform);
    Console.WriteLine("  Сценарий: запуск с платформы");
    Console.WriteLine($"  Начальная высота: {presetTrajectory.StartY:F2} м");
    Console.WriteLine($"  Время полёта: {presetTrajectory.GetFlightTime():F3} с");
    Console.WriteLine($"  Дальность: {presetTrajectory.GetRange():F3} м");

    Console.WriteLine("\n[4] Итог: расчёт выполнен, объект Trajectory корректно создан.");
}
catch (TrajectoryValidationException ex)
{
    Console.WriteLine("Ошибка проверки данных:");
    foreach (string error in ex.Errors)
    {
        Console.WriteLine($"  - {error}");
    }
}

static void PrintHeader()
{
    Console.WriteLine(new string('=', 72));
    Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №1 — ПОРОЖДАЮЩИЕ ПАТТЕРНЫ");
    Console.WriteLine("ВАРИАНТ 3 — «ТРАЕКТОРИЯ ДВИЖЕНИЯ»");
    Console.WriteLine(new string('=', 72));
}

static void PrintParameters(Trajectory trajectory)
{
    Console.WriteLine("\nПараметры созданной траектории:");
    Console.WriteLine($"  Положение старта: ({trajectory.StartX:F2}; {trajectory.StartY:F2}) м");
    Console.WriteLine($"  Начальная скорость: {trajectory.InitialSpeed:F2} м/с");
    Console.WriteLine($"  Угол запуска: {trajectory.LaunchAngleDegrees:F2}°");
    Console.WriteLine($"  Ускорение свободного падения: {trajectory.Gravity:F2} м/с²");
    Console.WriteLine($"  Шаг расчёта: {trajectory.TimeStep:F2} с");
}

static void PrintStatistics(Trajectory trajectory)
{
    Console.WriteLine("\nОсновные характеристики движения:");
    Console.WriteLine($"  Время полёта: {trajectory.GetFlightTime():F3} с");
    Console.WriteLine($"  Максимальная высота: {trajectory.GetMaximumHeight():F3} м");
    Console.WriteLine($"  Дальность: {trajectory.GetRange():F3} м");
}

static void PrintPoints(IReadOnlyList<TrajectoryPoint> points)
{
    Console.WriteLine("\nНесколько расчётных точек:");
    Console.WriteLine("  t, с |     X, м |     Y, м | Vy, м/с");
    Console.WriteLine("  ------+----------+----------+---------");

    for (int i = 0; i < points.Count; i += 3)
    {
        TrajectoryPoint point = points[i];
        Console.WriteLine(
            $"  {point.Time,5:F2} | {point.X,8:F3} | {point.Y,8:F3} | {point.VerticalVelocity,7:F3}");
    }

    TrajectoryPoint last = points[^1];
    if ((points.Count - 1) % 3 != 0)
    {
        Console.WriteLine(
            $"  {last.Time,5:F2} | {last.X,8:F3} | {last.Y,8:F3} | {last.VerticalVelocity,7:F3}");
    }
}
