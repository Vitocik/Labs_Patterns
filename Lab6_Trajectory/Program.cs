using System.Globalization;
using System.Text;
using Lab6.TrajectoryProxy.Core;
using Lab6.TrajectoryProxy.Models;
using Lab6.TrajectoryProxy.Proxy;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

Console.WriteLine("============================================================");
Console.WriteLine(" ЛАБОРАТОРНАЯ РАБОТА № 6 — ПАТТЕРН «ЗАМЕСТИТЕЛЬ» (PROXY)");
Console.WriteLine(" Предметная область: моделирование траектории движения тела");
Console.WriteLine("============================================================");
Console.WriteLine();

ITrajectoryService service = new TrajectoryCalculationProxy();
var proxy = (TrajectoryCalculationProxy)service;

Console.WriteLine($"Реальный сервис создан до первого запроса: {proxy.IsRealServiceCreated}");
Console.WriteLine();

var baseScenario = new TrajectoryParameters(
    ScenarioName: "Базовый сценарий",
    InitialX: 0.0,
    InitialY: 0.0,
    InitialSpeed: 20.0,
    LaunchAngleDegrees: 45.0,
    Gravity: 9.81,
    TimeStep: 0.5,
    WindSpeed: 0.0);

Console.WriteLine("[1] Первый запрос — базовая траектория");
var firstResult = service.Calculate(baseScenario);
PrintResult(firstResult);
Console.WriteLine($"Реальный сервис создан: {proxy.IsRealServiceCreated}");
Console.WriteLine();

Console.WriteLine("[2] Повторный запрос с теми же параметрами");
var repeatedResult = service.Calculate(baseScenario);
PrintResult(repeatedResult);
Console.WriteLine($"Возвращён тот же объект из кэша: {ReferenceEquals(firstResult, repeatedResult)}");
Console.WriteLine();

var windyScenario = baseScenario with
{
    ScenarioName = "Сценарий с попутным ветром",
    WindSpeed = 5.0
};

Console.WriteLine("[3] Новый запрос — попутный ветер +5 м/с");
var windyResult = service.Calculate(windyScenario);
PrintResult(windyResult);
Console.WriteLine();

Console.WriteLine("------------------- Статистика заместителя -------------------");
Console.WriteLine($"Фактических расчётов: {proxy.PhysicalCalculations}");
Console.WriteLine($"Попаданий в кэш:      {proxy.CacheHits}");
Console.WriteLine($"Результатов в кэше:   {proxy.CachedResults}");
Console.WriteLine();

var proxyCheckPassed =
    ReferenceEquals(firstResult, repeatedResult)
    && proxy.PhysicalCalculations == 2
    && proxy.CacheHits == 1
    && proxy.CachedResults == 2;

Console.WriteLine(proxyCheckPassed
    ? "Проверка паттерна Proxy: УСПЕШНО"
    : "Проверка паттерна Proxy: ОШИБКА");
Console.WriteLine("============================================================");

if (!proxyCheckPassed)
    Environment.ExitCode = 1;

static void PrintResult(TrajectoryResult result)
{
    Console.WriteLine($"  Сценарий:       {result.ScenarioName}");
    Console.WriteLine($"  Время полёта:   {result.FlightTimeSeconds:F3} с");
    Console.WriteLine($"  Макс. высота:   {result.MaxHeightMeters:F3} м");
    Console.WriteLine($"  Дальность:      {result.RangeMeters:F3} м");
    Console.WriteLine($"  Точек траектории: {result.Points.Count}");
}
