namespace TrajectoryMovementLab.Patterns;

/// <summary>
/// Единая точка хранения параметров моделирования.
/// Реализует паттерн "Одиночка" через ленивую потокобезопасную инициализацию.
/// </summary>
public sealed class SimulationSettings
{
    private static readonly Lazy<SimulationSettings> InstanceHolder =
        new(() => new SimulationSettings());

    private SimulationSettings()
    {
    }

    public static SimulationSettings Instance => InstanceHolder.Value;

    public double DefaultGravity { get; } = 9.81;
    public double DefaultInitialSpeed { get; } = 20.0;
    public double DefaultLaunchAngle { get; } = 45.0;
    public double DefaultTimeStep { get; } = 0.2;
}
