using TrajectoryMovementLab.Builders;
using TrajectoryMovementLab.Models;

namespace TrajectoryMovementLab.Directors;

/// <summary>
/// Определяет типовые последовательности вызовов строителя.
/// </summary>
public sealed class TrajectoryScenarioDirector
{
    private readonly TrajectoryBuilder _builder;

    public TrajectoryScenarioDirector(TrajectoryBuilder builder)
    {
        _builder = builder ?? throw new ArgumentNullException(nameof(builder));
    }

    public Trajectory Construct(TrajectoryPreset preset)
    {
        _builder.Reset();

        switch (preset)
        {
            case TrajectoryPreset.Standard:
                BuildStandard();
                break;
            case TrajectoryPreset.FromPlatform:
                BuildFromPlatform();
                break;
            case TrajectoryPreset.HighSpeed:
                BuildHighSpeed();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(preset));
        }

        return _builder.Build();
    }

    private void BuildStandard()
    {
        _builder
            .SetStartPosition(0, 0)
            .SetInitialSpeed(20)
            .SetLaunchAngle(45);
    }

    private void BuildFromPlatform()
    {
        _builder
            .SetStartPosition(0, 5)
            .SetInitialSpeed(18)
            .SetLaunchAngle(35);
    }

    private void BuildHighSpeed()
    {
        _builder
            .SetStartPosition(0, 0)
            .SetInitialSpeed(28)
            .SetLaunchAngle(40);
    }
}
