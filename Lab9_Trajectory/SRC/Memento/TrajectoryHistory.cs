namespace TrajectoryMovementLab9.Memento;

/// <summary>Caretaker: хранит снимки и управляет их нумерацией, не раскрывая содержимое.</summary>
public sealed class TrajectoryHistory
{
    private readonly List<TrajectoryMemento> _snapshots = [];

    public int Save(TrajectoryMemento memento)
    {
        ArgumentNullException.ThrowIfNull(memento);
        _snapshots.Add(memento);
        return _snapshots.Count;
    }

    public TrajectoryMemento Get(int number)
    {
        if (number < 1 || number > _snapshots.Count)
            throw new ArgumentOutOfRangeException(nameof(number), "Снимок с таким номером не существует.");

        return _snapshots[number - 1];
    }

    public IReadOnlyList<string> DescribeAll() => _snapshots
        .Select((snapshot, index) => $"{index + 1}. {snapshot.Description} ({snapshot.SavedAt:HH:mm:ss})")
        .ToArray();

    public int Count => _snapshots.Count;
}
