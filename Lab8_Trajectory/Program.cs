using System.Globalization;
using System.Text;
using Lab8.TrajectoryChainOfResponsibility.Core;
using Lab8.TrajectoryChainOfResponsibility.Models;
using Lab8.TrajectoryChainOfResponsibility.Validation;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

Console.WriteLine("============================================================");
Console.WriteLine(" ЛАБОРАТОРНАЯ РАБОТА № 8 — ЦЕПОЧКА ОБЯЗАННОСТЕЙ");
Console.WriteLine(" Предметная область: приложение «Траектория движения»");
Console.WriteLine("============================================================");
Console.WriteLine();

var service = new ValidatedTrajectoryService();
var baseScenario = new TrajectoryParameters(
    "Базовый сценарий", 0.0, 0.0, 20.0, 45.0, 9.81, 0.5, 0.0);

Console.WriteLine("[1] Корректные входные данные — базовая траектория");
var baseValidation = service.Validate(baseScenario);
PrintValidation(baseValidation);
var baseResult = service.Calculate(baseScenario);
PrintResult(baseResult);
Console.WriteLine();

Console.WriteLine("[2] Некорректный угол запуска — 95 градусов");
var invalidAngle = baseScenario with
{
    ScenarioName = "Некорректный угол",
    LaunchAngleDegrees = 95.0
};
TryCalculate(service, invalidAngle);
Console.WriteLine();

Console.WriteLine("[3] Слишком мелкий шаг дискретизации — 0,000001 с");
var tooFine = baseScenario with
{
    ScenarioName = "Слишком подробная дискретизация",
    TimeStep = 0.000001
};
TryCalculate(service, tooFine);
Console.WriteLine();

Console.WriteLine("[4] Корректный сценарий с попутным ветром +5 м/с");
var windyScenario = baseScenario with
{
    ScenarioName = "Сценарий с попутным ветром",
    WindSpeed = 5.0
};
var windyValidation = service.Validate(windyScenario);
PrintValidation(windyValidation);
var windyResult = service.Calculate(windyScenario);
PrintResult(windyResult);
Console.WriteLine();

var checksPassed = baseValidation.IsValid
    && !invalidAngle.Equals(baseScenario)
    && service.Validate(invalidAngle).FailedHandler == nameof(PhysicalMovementHandler)
    && service.Validate(tooFine).FailedHandler == nameof(TrajectorySamplingHandler)
    && windyValidation.IsValid
    && service.SuccessfulCalculations == 2;

Console.WriteLine("---------------------- Статистика цепочки ----------------------");
Console.WriteLine($"Успешных расчётов: {service.SuccessfulCalculations}");
Console.WriteLine("Отклонённых сценариев: 2");
Console.WriteLine("Проверка Chain of Responsibility: "
    + (checksPassed ? "УСПЕШНО" : "ОШИБКА"));
Console.WriteLine("---------------------------------------------------------------");

if (!checksPassed)
    Environment.ExitCode = 1;

static void TryCalculate(ValidatedTrajectoryService service, TrajectoryParameters parameters)
{
    try
    {
        _ = service.Calculate(parameters);
        Console.WriteLine("  Ошибка проверки: сценарий неожиданно принят.");
    }
    catch (TrajectoryValidationException exception)
    {
        PrintValidation(exception.Validation);
        Console.WriteLine("  Расчёт не запускался: цепочка остановила запрос на первом нарушении.");
    }
}

static void PrintValidation(TrajectoryValidationResult validation)
{
    Console.WriteLine("  Пройдены обработчики: "
        + (validation.PassedHandlers.Count == 0
            ? "нет"
            : string.Join(" -> ", validation.PassedHandlers)));
    if (validation.IsValid)
        Console.WriteLine("  Результат: все этапы пройдены.");
    else
        Console.WriteLine($"  Остановлено в {validation.FailedHandler}: {validation.Message}");
}

static void PrintResult(TrajectoryResult result)
{
    Console.WriteLine($"  Сценарий:           {result.ScenarioName}");
    Console.WriteLine($"  Время полёта:       {result.FlightTimeSeconds:F3} с");
    Console.WriteLine($"  Максимальная высота: {result.MaxHeightMeters:F3} м");
    Console.WriteLine($"  Дальность:          {result.RangeMeters:F3} м");
    Console.WriteLine($"  Точек траектории:   {result.Points.Count}");
}
