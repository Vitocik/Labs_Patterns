using Lab5_Trajectory_Variant3.Models;

namespace Lab5_Trajectory_Variant3.Flyweight;

/// <summary>Получает общие стили извне; координаты точки остаются внешними данными.</summary>
public sealed class TrajectoryRenderer
{
    public void DisplaySamples(TrajectoryResult result, TrajectoryStyle style, int count = 4)
    {
        Console.WriteLine($"  {style.Symbol} {result.Scenario.Name} — стиль «{style.Label}» ({style.ColorName})");
        Console.WriteLine("    t, с     x, м      y, м");
        foreach (var p in result.Points.Take(count))
            Console.WriteLine($"    {p.TimeSeconds,5:F2}  {p.X,7:F2}  {p.Y,7:F2}");
    }
}
