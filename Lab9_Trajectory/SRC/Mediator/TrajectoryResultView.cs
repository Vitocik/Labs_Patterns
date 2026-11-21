using TrajectoryMovementLab9.Models;

namespace TrajectoryMovementLab9.Mediator;

/// <summary>Коллега-представление: отображает результаты и сообщения, не управляя расчётом.</summary>
public sealed class TrajectoryResultView
{
    public void ShowParameters(TrajectoryParameters parameters) =>
        Console.WriteLine($"Параметры приняты: {parameters.Name}, v0 = {parameters.InitialSpeed:F1} м/с, " +
                          $"α = {parameters.LaunchAngleDeg:F1}°, ветер = {parameters.WindSpeed:+0.0;-0.0;0.0} м/с");

    public void ShowResult(TrajectoryResult result)
    {
        Console.WriteLine($"Модель: {result.ModelName}");
        Console.WriteLine($"Время полёта: {result.FlightTime:F3} с");
        Console.WriteLine($"Максимальная высота: {result.MaximumHeight:F3} м");
        Console.WriteLine($"Дальность: {result.Range:F3} м");
        Console.WriteLine($"Рассчитано точек: {result.Points.Count}");
    }

    public void ShowMessage(string message) => Console.WriteLine(message);
}
