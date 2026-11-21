namespace TrajectoryMovementLab10.Observer;

/// <summary>Сообщает, если рассчитанная дальность превышает контрольное значение.</summary>
public sealed class LongRangeObserver(double threshold) : ITrajectoryObserver
{
    public void Update(TrajectoryEvent notification)
    {
        if (notification.Type == TrajectoryEventType.ResultReady &&
            notification.Result is not null &&
            notification.Result.Range > threshold)
        {
            Console.WriteLine(
                $"[Наблюдатель: контроль дальности] ВНИМАНИЕ: дальность " +
                $"{notification.Result.Range:F3} м превышает порог {threshold:F1} м.");
        }
    }
}
