using System.Globalization;
using System.Text;
using TrajectoryMovementLab10.Calculation;
using TrajectoryMovementLab10.Models;
using TrajectoryMovementLab10.Observer;
using TrajectoryMovementLab10.State;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

Console.WriteLine(new string('=', 76));
Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №10 — ПОВЕДЕНЧЕСКИЕ ПАТТЕРНЫ");
Console.WriteLine("ВАРИАНТ 3 — «ТРАЕКТОРИЯ ДВИЖЕНИЯ»");
Console.WriteLine("Паттерны: «Наблюдатель» и «Состояние»");
Console.WriteLine(new string('=', 76));

var session = new TrajectorySession();
var calculator = new TrajectoryCalculationEngine();
var consoleObserver = new ConsoleTrajectoryObserver();
var auditObserver = new TrajectoryAuditObserver();
var rangeObserver = new LongRangeObserver(50.0);

session.Attach(consoleObserver);
session.Attach(auditObserver);
session.Attach(rangeObserver);

var baseParameters = new TrajectoryParameters(
    "Базовый бросок", 0.0, 0.0, 20.0, 45.0, 9.81, 0.5, 0.0);

Console.WriteLine($"\nНачальное состояние: {session.StateName}");
Console.WriteLine("\n[1] Настройка и выполнение базового сценария");
session.Configure(baseParameters);
TrajectoryResult baseResult = session.Calculate(calculator);
PrintResult(baseResult);

Console.WriteLine("\n[2] Изменение параметров и повторный расчёт с попутным ветром");
var windyParameters = baseParameters with
{
    Name = "Попутный ветер",
    WindSpeed = 5.0
};
session.Configure(windyParameters);
TrajectoryResult windyResult = session.Calculate(calculator);
PrintResult(windyResult);

Console.WriteLine("\n[3] Сводка наблюдателя-журнала");
auditObserver.PrintSummary();

Console.WriteLine("\n[4] Отписка журнала и сброс конечного автомата");
session.Detach(auditObserver);
session.Reset();
Console.WriteLine($"Итоговое состояние: {session.StateName}");
Console.WriteLine("Демонстрация завершена.");

static void PrintResult(TrajectoryResult result)
{
    Console.WriteLine($"Время полёта: {result.FlightTime:F3} с");
    Console.WriteLine($"Максимальная высота: {result.MaximumHeight:F3} м");
    Console.WriteLine($"Дальность: {result.Range:F3} м");
    Console.WriteLine($"Точек траектории: {result.Points.Count}");
}
