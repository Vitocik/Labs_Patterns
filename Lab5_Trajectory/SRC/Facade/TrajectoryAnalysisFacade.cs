using Lab5_Trajectory_Variant3.Core;
using Lab5_Trajectory_Variant3.Flyweight;
using Lab5_Trajectory_Variant3.Models;

namespace Lab5_Trajectory_Variant3.Facade;

/// <summary>Упрощённая точка входа: скрывает проверку, расчёт, кэш стилей и вывод.</summary>
public sealed class TrajectoryAnalysisFacade
{
    private readonly TrajectoryCalculator _calculator = new();
    private readonly TrajectoryStyleFactory _styles = new();
    private readonly TrajectoryRenderer _renderer = new();

    public void RunStandardComparison()
    {
        var scenarios = new[]
        {
            new TrajectoryScenario("Базовый бросок", 20, 45),
            new TrajectoryScenario("Угол запуска 60°", 20, 60),
            new TrajectoryScenario("Попутный ветер +5 м/с", 20, 45, WindSpeed: 5)
        };
        var results = new List<(TrajectoryResult Result, TrajectoryStyle Style)>();
        foreach (var scenario in scenarios)
        {
            TrajectoryValidator.Validate(scenario);
            string key = scenario.WindSpeed == 0 ? (scenario.LaunchAngleDegrees > 50 ? "high" : "ideal") : "wind";
            var result = _calculator.Calculate(scenario);
            results.Add((result, _styles.GetStyle(key)));
        }

        Console.WriteLine("Сравнение рассчитанных сценариев:");
        Console.WriteLine("Сценарий                         T, с     Hmax, м   Дальность, м");
        foreach (var item in results)
            Console.WriteLine($"{item.Result.Scenario.Name,-31} {item.Result.FlightTimeSeconds,6:F3} " +
                              $"{item.Result.MaximumHeight,9:F3} {item.Result.RangeMeters,13:F3}");

        Console.WriteLine("\nПримеры координат (стиль приспособленца передаётся как общий объект):");
        foreach (var item in results) _renderer.DisplaySamples(item.Result, item.Style);

        var styleA = _styles.GetStyle("ideal");
        var styleB = _styles.GetStyle("ideal");
        Console.WriteLine($"\nОдин объект стиля используется повторно: {ReferenceEquals(styleA, styleB)}");
        Console.WriteLine($"Количество созданных стилей: {_styles.CreatedCount} (при трёх сценариях).");
    }
}
