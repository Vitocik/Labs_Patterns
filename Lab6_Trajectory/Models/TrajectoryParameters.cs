namespace Lab6.TrajectoryProxy.Models;

/// <summary>
/// Полный набор входных параметров одного сценария движения.
/// record обеспечивает сравнение экземпляров по значениям всех полей,
/// поэтому объект можно безопасно использовать как ключ словаря кэша.
/// </summary>
public sealed record TrajectoryParameters(
    string ScenarioName,
    double InitialX,
    double InitialY,
    double InitialSpeed,
    double LaunchAngleDegrees,
    double Gravity,
    double TimeStep,
    double WindSpeed);
