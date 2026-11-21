namespace TrajectoryMovementLab10.Observer;

/// <summary>Сохраняет последовательность событий для последующего просмотра.</summary>
public sealed class TrajectoryAuditObserver : ITrajectoryObserver
{
    private readonly List<TrajectoryEvent> _events = [];

    public int EventCount => _events.Count;

    public void Update(TrajectoryEvent notification) => _events.Add(notification);

    public void PrintSummary()
    {
        Console.WriteLine($"[Наблюдатель: журнал] Зафиксировано событий: {_events.Count}.");
        for (int index = 0; index < _events.Count; index++)
            Console.WriteLine($"  {index + 1}. {_events[index].Message}");
    }
}
