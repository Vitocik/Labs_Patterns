using TrajectoryMovementLab.Models;

namespace TrajectoryMovementLab.Bridge;

/// <summary>Уточнённая абстракция: выводит только основные характеристики.</summary>
public sealed class SummaryTrajectorySimulator : TrajectorySimulator
{
    public SummaryTrajectorySimulator(IFlightModel model) : base(model) { }

    protected override void Display(
        TrajectoryParameters parameters,
        TrajectoryResult result)
    {
        Console.WriteLine($"\n--- КРАТКАЯ СВОДКА: {parameters.Name} ---");
        Console.WriteLine($"Модель: {result.ModelName}");
        Console.WriteLine($"Время полёта: {result.FlightTime:F3} с");
        Console.WriteLine($"Максимальная высота: {result.MaximumHeight:F3} м");
        Console.WriteLine($"Дальность: {result.Range:F3} м");
    }
}
