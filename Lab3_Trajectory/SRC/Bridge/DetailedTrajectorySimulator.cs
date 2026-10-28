using TrajectoryMovementLab.Models;

namespace TrajectoryMovementLab.Bridge;

/// <summary>Уточнённая абстракция: выводит сводку и таблицу расчётных точек.</summary>
public sealed class DetailedTrajectorySimulator : TrajectorySimulator
{
    public DetailedTrajectorySimulator(IFlightModel model) : base(model) { }

    protected override void Display(
        TrajectoryParameters parameters,
        TrajectoryResult result)
    {
        Console.WriteLine($"\n--- ПОДРОБНЫЙ РАСЧЁТ: {parameters.Name} ---");
        Console.WriteLine($"Модель: {result.ModelName}");
        Console.WriteLine($"Время полёта: {result.FlightTime:F3} с; " +
                          $"макс. высота: {result.MaximumHeight:F3} м; " +
                          $"дальность: {result.Range:F3} м");
        Console.WriteLine("   t, с |      x, м |      y, м");
        Console.WriteLine("--------+-----------+----------");

        // Выводим максимум девять равномерно набранных точек, чтобы таблица была компактной.
        int count = Math.Min(result.Points.Count, 9);
        for (int i = 0; i < count; i++)
        {
            TrajectoryPoint point = result.Points[i];
            Console.WriteLine($"{point.Time,7:F2} | {point.X,9:F3} | {point.Y,8:F3}");
        }
        if (result.Points.Count > count)
            Console.WriteLine("... остальные точки рассчитаны, но скрыты для краткости.");
    }
}
