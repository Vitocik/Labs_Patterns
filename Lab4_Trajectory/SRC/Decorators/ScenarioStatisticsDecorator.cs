using Lab4_Trajectory_Variant3.Composite;
using Lab4_Trajectory_Variant3.Models;

namespace Lab4_Trajectory_Variant3.Decorators;

/// <summary>Добавляет сводную статистику к любому компоненту.</summary>
public sealed class ScenarioStatisticsDecorator : TrajectoryComponentDecorator
{
    public ScenarioStatisticsDecorator(ITrajectoryComponent inner) : base(inner) { }

    protected override void DisplaySupplement(IReadOnlyList<TrajectoryResult> results, int indent)
    {
        if (results.Count == 0) return;
        string pad = new(' ', indent * 2);
        Console.WriteLine($"{pad}  Статистика: сценариев={results.Count}; " +
                          $"максимальная дальность={results.Max(r => r.RangeMeters):F3} м");
    }
}
