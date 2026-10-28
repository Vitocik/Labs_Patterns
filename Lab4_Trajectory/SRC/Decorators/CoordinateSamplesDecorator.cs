using Lab4_Trajectory_Variant3.Composite;
using Lab4_Trajectory_Variant3.Models;

namespace Lab4_Trajectory_Variant3.Decorators;

/// <summary>Добавляет координатную таблицу первого сценария.</summary>
public sealed class CoordinateSamplesDecorator : TrajectoryComponentDecorator
{
    public CoordinateSamplesDecorator(ITrajectoryComponent inner) : base(inner) { }

    protected override void DisplaySupplement(IReadOnlyList<TrajectoryResult> results, int indent)
    {
        if (results.Count == 0) return;
        string pad = new(' ', indent * 2);
        var first = results[0];
        Console.WriteLine($"{pad}  Координаты: {first.Scenario.Name}");
        Console.WriteLine($"{pad}  t, с    x, м      y, м");
        foreach (var point in first.Points.Take(5))
            Console.WriteLine($"{pad}  {point.TimeSeconds,5:F2}  {point.X,7:F2}  {point.Y,7:F2}");
    }
}
