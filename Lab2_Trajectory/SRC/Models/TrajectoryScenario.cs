namespace TrajectoryMovementLab.Models;

using TrajectoryMovementLab.Patterns;

/// <summary>Настроенный сценарий запуска, выступающий Concrete Prototype.</summary>
public sealed class TrajectoryScenario : IPrototype<TrajectoryScenario>
{
    public TrajectoryScenario(
        string name,
        double x0,
        double y0,
        double initialSpeed,
        double launchAngleDeg,
        double gravity,
        double timeStep,
        double windSpeed)
    {
        Name = name;
        X0 = x0;
        Y0 = y0;
        InitialSpeed = initialSpeed;
        LaunchAngleDeg = launchAngleDeg;
        Gravity = gravity;
        TimeStep = timeStep;
        WindSpeed = windSpeed;
    }

    public string Name { get; set; }
    public double X0 { get; set; }
    public double Y0 { get; set; }
    public double InitialSpeed { get; set; }
    public double LaunchAngleDeg { get; set; }
    public double Gravity { get; set; }
    public double TimeStep { get; set; }
    public double WindSpeed { get; set; }

    public TrajectoryScenario Clone()
    {
        return new TrajectoryScenario(
            Name, X0, Y0, InitialSpeed, LaunchAngleDeg,
            Gravity, TimeStep, WindSpeed);
    }
}
