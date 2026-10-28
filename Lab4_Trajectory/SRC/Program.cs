using Lab4_Trajectory_Variant3.Composite;
using Lab4_Trajectory_Variant3.Decorators;
using Lab4_Trajectory_Variant3.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine(new string('=', 76));
Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №4 — КОМПОНОВЩИК И ДЕКОРАТОР");
Console.WriteLine("ВАРИАНТ 3 — «ТРАЕКТОРИЯ ДВИЖЕНИЯ»");
Console.WriteLine(new string('=', 76));

var root = new TrajectoryGroup("Сравнение траекторий");
root.Add(new TrajectoryLeaf(new TrajectoryScenario("Базовый бросок", 20, 45)));

var angleGroup = new TrajectoryGroup("Влияние угла запуска");
angleGroup.Add(new TrajectoryLeaf(new TrajectoryScenario("Угол 30 градусов", 20, 30)));
angleGroup.Add(new TrajectoryLeaf(new TrajectoryScenario("Угол 60 градусов", 20, 60)));
root.Add(angleGroup);
root.Add(new TrajectoryLeaf(new TrajectoryScenario("Попутный ветер +5 м/с", 20, 45, WindSpeed: 5)));

ITrajectoryComponent decorated =
    new CoordinateSamplesDecorator(new ScenarioStatisticsDecorator(root));

Console.WriteLine("
Иерархия компонентов и результаты расчёта:");
decorated.Display();

Console.WriteLine("
Проверка Composite: количество результатов = " + decorated.Calculate().Count);
Console.WriteLine("Проверка Decorator: к выводу добавлены статистика и координаты без изменения листьев.");
Console.WriteLine("
Нажмите Enter для завершения.");
Console.ReadLine();
