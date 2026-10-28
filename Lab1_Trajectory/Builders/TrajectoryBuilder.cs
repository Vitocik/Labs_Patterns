using TrajectoryMovementLab.Exceptions;
using TrajectoryMovementLab.Models;
using TrajectoryMovementLab.Patterns;

namespace TrajectoryMovementLab.Builders;

/// <summary>
/// Пошагово формирует объект Trajectory.
/// </summary>
public sealed class TrajectoryBuilder
{
    private double _startX;
    private double _startY;
    private double _initialSpeed;
    private double _launchAngleDegrees;
    private double _gravity;
    private double _timeStep;

    public TrajectoryBuilder()
    {
        Reset();
    }

    public TrajectoryBuilder Reset()
    {
        SimulationSettings settings = SimulationSettings.Instance;
        _startX = 0;
        _startY = 0;
        _initialSpeed = settings.DefaultInitialSpeed;
        _launchAngleDegrees = settings.DefaultLaunchAngle;
        _gravity = settings.DefaultGravity;
        _timeStep = settings.DefaultTimeStep;
        return this;
    }

    public TrajectoryBuilder SetStartPosition(double x, double y)
    {
        _startX = x;
        _startY = y;
        return this;
    }

    public TrajectoryBuilder SetInitialSpeed(double speed)
    {
        _initialSpeed = speed;
        return this;
    }

    public TrajectoryBuilder SetLaunchAngle(double degrees)
    {
        _launchAngleDegrees = degrees;
        return this;
    }

    public TrajectoryBuilder SetGravity(double gravity)
    {
        _gravity = gravity;
        return this;
    }

    public TrajectoryBuilder SetTimeStep(double step)
    {
        _timeStep = step;
        return this;
    }

    public Trajectory Build()
    {
        Validate();

        return new Trajectory(
            _startX,
            _startY,
            _initialSpeed,
            _launchAngleDegrees,
            _gravity,
            _timeStep);
    }

    private void Validate()
    {
        var errors = new List<string>();

        if (_startY < 0)
        {
            errors.Add("Начальная высота не может быть отрицательной.");
        }

        if (_initialSpeed <= 0)
        {
            errors.Add("Начальная скорость должна быть больше нуля.");
        }

        if (_launchAngleDegrees <= 0 || _launchAngleDegrees >= 90)
        {
            errors.Add("Угол запуска должен быть больше 0 и меньше 90 градусов.");
        }

        if (_gravity <= 0)
        {
            errors.Add("Ускорение свободного падения должно быть больше нуля.");
        }

        if (_timeStep <= 0)
        {
            errors.Add("Шаг расчёта должен быть больше нуля.");
        }

        if (errors.Count > 0)
        {
            throw new TrajectoryValidationException(errors);
        }
    }
}
