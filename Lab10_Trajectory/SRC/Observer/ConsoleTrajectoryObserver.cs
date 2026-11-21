namespace TrajectoryMovementLab10.Observer;

/// <summary>Выводит уведомления о состоянии расчёта в консоль.</summary>
public sealed class ConsoleTrajectoryObserver : ITrajectoryObserver
{
    public void Update(TrajectoryEvent notification) =>
        Console.WriteLine($"[Наблюдатель: консоль] {notification.Message}");
}
