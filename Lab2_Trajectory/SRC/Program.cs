using System.Text;
using TrajectoryMovementLab.Factories;
using TrajectoryMovementLab.Models;
using TrajectoryMovementLab.Patterns;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine(new string('=', 78));
Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №2 — ПОРОЖДАЮЩИЕ ПАТТЕРНЫ");
Console.WriteLine("ВАРИАНТ 3 — «ТРАЕКТОРИЯ ДВИЖЕНИЯ»");
Console.WriteLine("Абстрактная фабрика + Прототип");
Console.WriteLine(new string('=', 78));

// Prototype: базовый сценарий хранится в реестре как прототип.
var registry = new ScenarioPrototypeRegistry();

registry.Register("base", new TrajectoryScenario(
    name: "Базовый бросок",
    x0: 0,
    y0: 0,
    initialSpeed: 20,
    launchAngleDeg: 45,
    gravity: 9.81,
    timeStep: 0.2,
    windSpeed: 0));

var baseScenario = registry.Create("base");

var highArc = baseScenario.Clone();
highArc.Name = "Высокая траектория";
highArc.LaunchAngleDeg = 60;

var tailwind = baseScenario.Clone();
tailwind.Name = "Попутный ветер";
tailwind.WindSpeed = 5;

Console.WriteLine("\n[1] ПРОВЕРКА ПАТТЕРНА «ПРОТОТИП»");

Console.WriteLine(
    $"  Реестр возвращает копию: " +
    $"{!ReferenceEquals(baseScenario, registry.Create("base"))}");

Console.WriteLine(
    $"  Клон не является тем же объектом: " +
    $"{!ReferenceEquals(baseScenario, highArc)}");

Console.WriteLine(
    $"  Базовый угол после изменения клона: " +
    $"{baseScenario.LaunchAngleDeg:F1}°");

Console.WriteLine(
    $"  Угол клона: {highArc.LaunchAngleDeg:F1}°");

Console.WriteLine(
    $"  Скорость клона с ветром: " +
    $"{tailwind.WindSpeed:F1} м/с по направлению +X");

Console.WriteLine("\n[2] ПРОВЕРКА ПАТТЕРНА «АБСТРАКТНАЯ ФАБРИКА»");

RunFactoryDemo(
    "Идеальная модель",
    new IdealMotionFactory(),
    baseScenario);

RunFactoryDemo(
    "Модель с попутным ветром",
    new WindMotionFactory(),
    tailwind);

Console.WriteLine(
    "\n[3] СРАВНЕНИЕ СЦЕНАРИЕВ, СОЗДАННЫХ ИЗ ОДНОГО ПРОТОТИПА");

PrintScenarioSummary(
    highArc,
    new IdealMotionFactory());

PrintScenarioSummary(
    tailwind,
    new WindMotionFactory());

Console.WriteLine("\nГотово. Нажмите Enter для завершения.");
Console.ReadLine();

static void RunFactoryDemo(
    string title,
    IMotionModelFactory factory,
    TrajectoryScenario scenario)
{
    var calculator = factory.CreateCalculator();
    var formatter = factory.CreateFormatter();

    var result = calculator.Calculate(scenario);

    Console.WriteLine($"\n--- {title} ---");
    Console.WriteLine(formatter.Format(scenario, result));
}

static void PrintScenarioSummary(
    TrajectoryScenario scenario,
    IMotionModelFactory factory)
{
    var result = factory
        .CreateCalculator()
        .Calculate(scenario);

    Console.WriteLine(
        $"  {scenario.Name}: " +
        $"угол {scenario.LaunchAngleDeg:F0}°, " +
        $"ветер {scenario.WindSpeed:F1} м/с, " +
        $"дальность {result.Range:F3} м, " +
        $"макс. высота {result.MaximumHeight:F3} м");
}