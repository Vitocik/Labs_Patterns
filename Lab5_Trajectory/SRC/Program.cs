using Lab5_Trajectory_Variant3.Facade;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine(new string('=', 76));
Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №5 — ФАСАД И ПРИСПОСОБЛЕНЕЦ");
Console.WriteLine("ВАРИАНТ 3 — «ТРАЕКТОРИЯ ДВИЖЕНИЯ»");
Console.WriteLine(new string('=', 76));

// Клиент вызывает один метод; все подсистемы скрыты внутри фасада.
var facade = new TrajectoryAnalysisFacade();
facade.RunStandardComparison();

Console.WriteLine("\nНажмите Enter для завершения.");
Console.ReadLine();
