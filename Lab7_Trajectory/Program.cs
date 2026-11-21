using System.Globalization;
using System.Text;
using Lab7.TrajectoryCommandIterator.Command;
using Lab7.TrajectoryCommandIterator.Core;
using Lab7.TrajectoryCommandIterator.Iterator;
using Lab7.TrajectoryCommandIterator.Models;
using Lab7.TrajectoryCommandIterator.Session;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

Console.WriteLine("============================================================");
Console.WriteLine(" ЛАБОРАТОРНАЯ РАБОТА № 7 — КОМАНДА И ИТЕРАТОР");
Console.WriteLine(" Предметная область: приложение «Траектория движения»");
Console.WriteLine("============================================================");
Console.WriteLine();

ITrajectoryService service = new RealTrajectoryService();
var session = new TrajectorySession();
var invoker = new CommandInvoker();

var baseScenario = new TrajectoryParameters(
    "Базовый сценарий", 0.0, 0.0, 20.0, 45.0, 9.81, 0.5, 0.0);
var windyScenario = baseScenario with
{
    ScenarioName = "Сценарий с попутным ветром",
    WindSpeed = 5.0
};

Console.WriteLine("[1] Выполнение команды расчёта базовой траектории");
var baseCommand = new CalculateTrajectoryCommand(service, session, baseScenario);
invoker.ExecuteCommand(baseCommand);
PrintResult(baseCommand.Result!);
Console.WriteLine($"Команд в стеке отмены: {invoker.ExecutedCommandCount}");
Console.WriteLine();

Console.WriteLine("[2] Выполнение команды расчёта сценария с ветром +5 м/с");
var windyCommand = new CalculateTrajectoryCommand(service, session, windyScenario);
invoker.ExecuteCommand(windyCommand);
PrintResult(windyCommand.Result!);
Console.WriteLine($"Команд в стеке отмены: {invoker.ExecutedCommandCount}");
Console.WriteLine();

Console.WriteLine("[3] Обход точек базовой траектории с помощью итератора");
var path = new TrajectoryPath(baseCommand.Result!.Points);
var visitedPoints = 0;
foreach (var point in path)
{
    Console.WriteLine($"  t = {point.TimeSeconds,6:F3} с | x = {point.X,8:F3} м | y = {point.Y,8:F3} м");
    visitedPoints++;
}
Console.WriteLine($"Всего последовательно обработано точек: {visitedPoints}");
Console.WriteLine();

Console.WriteLine("[4] Отмена последней команды");
var undoSucceeded = invoker.UndoLastCommand();
Console.WriteLine($"Команда отменена: {undoSucceeded}");
Console.WriteLine($"Сценариев, оставшихся в сессии: {session.Results.Count}");
Console.WriteLine($"Последний сценарий в сессии: {session.Results[^1].ScenarioName}");
Console.WriteLine();

var commandPassed = undoSucceeded
    && session.Results.Count == 1
    && ReferenceEquals(session.Results[0], baseCommand.Result)
    && invoker.ExecutedCommandCount == 1;
var iteratorPassed = visitedPoints == baseCommand.Result!.Points.Count
    && visitedPoints > 1;

Console.WriteLine("----------------------- Проверка паттернов -----------------------");
Console.WriteLine($"Проверка Command:  {(commandPassed ? "УСПЕШНО" : "ОШИБКА")}");
Console.WriteLine($"Проверка Iterator: {(iteratorPassed ? "УСПЕШНО" : "ОШИБКА")}");
Console.WriteLine("------------------------------------------------------------------");

if (!commandPassed || !iteratorPassed)
    Environment.ExitCode = 1;

static void PrintResult(TrajectoryResult result)
{
    Console.WriteLine($"  Сценарий:         {result.ScenarioName}");
    Console.WriteLine($"  Время полёта:     {result.FlightTimeSeconds:F3} с");
    Console.WriteLine($"  Максимальная высота: {result.MaxHeightMeters:F3} м");
    Console.WriteLine($"  Дальность:        {result.RangeMeters:F3} м");
    Console.WriteLine($"  Точек траектории: {result.Points.Count}");
}
