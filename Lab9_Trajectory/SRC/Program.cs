using System.Globalization;
using System.Text;
using TrajectoryMovementLab9.Mediator;
using TrajectoryMovementLab9.Models;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

Console.WriteLine(new string('=', 76));
Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №9 — ПОВЕДЕНЧЕСКИЕ ПАТТЕРНЫ");
Console.WriteLine("ВАРИАНТ 3 — «ТРАЕКТОРИЯ ДВИЖЕНИЯ»");
Console.WriteLine("Паттерны: «Посредник» и «Снимок»");
Console.WriteLine(new string('=', 76));

var mediator = new TrajectoryMediator();
var baseParameters = new TrajectoryParameters(
    "Базовый бросок", 0.0, 0.0, 20.0, 45.0, 9.81, 0.5, 0.0);

Console.WriteLine("\n[1] ПАТТЕРН «ПОСРЕДНИК»: ввод параметров и расчёт");
mediator.Input.Submit(baseParameters);
mediator.Input.Calculate();

Console.WriteLine("\n[2] ПАТТЕРН «СНИМОК»: сохранение первого состояния");
mediator.HistoryPanel.SaveSnapshot();

Console.WriteLine("\n[3] Изменение сценария через тот же интерфейс коллеги");
var windyParameters = baseParameters with
{
    Name = "Попутный ветер",
    WindSpeed = 5.0
};
mediator.Input.Submit(windyParameters);
mediator.Input.Calculate();
mediator.HistoryPanel.SaveSnapshot();
mediator.HistoryPanel.ShowHistory();

Console.WriteLine("\n[4] Восстановление первого снимка без повторного расчёта");
mediator.HistoryPanel.RestoreSnapshot(1);

Console.WriteLine("\nДемонстрация завершена.");
