using System.Globalization;
using System.Text;
using TrajectoryMovementLab.Adapter;
using TrajectoryMovementLab.Bridge;
using TrajectoryMovementLab.Models;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

Console.WriteLine(new string('=', 76));
Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №3 — СТРУКТУРНЫЕ ПАТТЕРНЫ");
Console.WriteLine("ВАРИАНТ 3 — «ТРАЕКТОРИЯ ДВИЖЕНИЯ»");
Console.WriteLine("Паттерны: «Мост» и «Адаптер»");
Console.WriteLine(new string('=', 76));

var baseParameters = new TrajectoryParameters(
    Name: "Базовый бросок",
    StartX: 0.0,
    StartY: 0.0,
    InitialSpeed: 20.0,
    LaunchAngleDeg: 45.0,
    Gravity: 9.81,
    TimeStep: 0.5,
    WindSpeed: 0.0);

Console.WriteLine("\n[1] ПАТТЕРН «МОСТ»: краткое представление + идеальная модель");
var summarySimulator = new SummaryTrajectorySimulator(new IdealBallisticModel());
summarySimulator.Simulate(baseParameters);

Console.WriteLine("\n[2] ПАТТЕРН «МОСТ»: подробное представление + модель с ветром");
var windyParameters = baseParameters with
{
    Name = "Попутный ветер",
    WindSpeed = 5.0
};
var detailedWindSimulator = new DetailedTrajectorySimulator(new WindAffectedModel());
detailedWindSimulator.Simulate(windyParameters);

Console.WriteLine("\n[3] ПАТТЕРН «АДАПТЕР»: подключение старого движка через IFlightModel");
var legacyParameters = baseParameters with
{
    Name = "Расчёт через старый движок",
    WindSpeed = -3.0
};
Console.WriteLine("LegacyTrajectoryEngine подключён через LegacyTrajectoryAdapter.");
var adapter = new LegacyTrajectoryAdapter(new LegacyTrajectoryEngine());
var summaryLegacySimulator = new SummaryTrajectorySimulator(adapter);
summaryLegacySimulator.Simulate(legacyParameters);

Console.WriteLine("\nДемонстрация завершена. Нажмите Enter для выхода.");
Console.ReadLine();
