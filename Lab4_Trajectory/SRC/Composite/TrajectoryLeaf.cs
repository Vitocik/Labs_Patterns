using Lab4_Trajectory_Variant3.Models;

namespace Lab4_Trajectory_Variant3.Composite;

/// <summary>Лист дерева: один сценарий траектории.</summary>
public sealed class TrajectoryLeaf : ITrajectoryComponent
{
    private readonly TrajectoryScenario _scenario;
    public string Name => _scenario.Name;

    public TrajectoryLeaf(TrajectoryScenario scenario) =>
        _scenario = scenario ?? throw new ArgumentNullException(nameof(scenario));

    public IReadOnlyList<TrajectoryResult> Calculate() =>
        new[] { TrajectoryCalculator.Calculate(_scenario) };

    public void Display(int indent = 0)
    {
        var result = Calculate()[0];
        string pad = new(' ', indent * 2);
        Console.WriteLine($"{pad}- {Name}: T={result.FlightTimeSeconds:F3} с; " +
                          $"Hmax={result.MaximumHeight:F3} м; L={result.RangeMeters:F3} м");
    }
}
