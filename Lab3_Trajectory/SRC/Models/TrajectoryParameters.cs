namespace TrajectoryMovementLab.Models;

/// <summary>
/// Исходные данные одного запуска: положение старта, скорость, угол,
/// ускорение свободного падения, шаг вывода и горизонтальная скорость ветра.
/// Угол задаётся в градусах.
/// </summary>
public sealed record TrajectoryParameters(
    string Name,
    double StartX,
    double StartY,
    double InitialSpeed,
    double LaunchAngleDeg,
    double Gravity,
    double TimeStep,
    double WindSpeed = 0.0);
