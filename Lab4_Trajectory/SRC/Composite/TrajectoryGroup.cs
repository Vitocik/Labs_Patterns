using Lab4_Trajectory_Variant3.Models;

namespace Lab4_Trajectory_Variant3.Composite;

/// <summary>Компоновщик: содержит листья и другие группы.</summary>
public sealed class TrajectoryGroup : ITrajectoryComponent
{
    private readonly List<ITrajectoryComponent> _children = new();
    public string Name { get; }
    public IReadOnlyList<ITrajectoryComponent> Children => _children.AsReadOnly();

    public TrajectoryGroup(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Укажите название группы.", nameof(name));
        Name = name;
    }

    public void Add(ITrajectoryComponent child)
    {
        ArgumentNullException.ThrowIfNull(child);
        _children.Add(child);
    }

    public bool Remove(ITrajectoryComponent child) => _children.Remove(child);

    public IReadOnlyList<TrajectoryResult> Calculate() =>
        _children.SelectMany(child => child.Calculate()).ToArray();

    public void Display(int indent = 0)
    {
        Console.WriteLine($"{new string(' ', indent * 2)}+ Группа: {Name}");
        foreach (var child in _children) child.Display(indent + 1);
    }
}
