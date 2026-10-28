using Lab4_Trajectory_Variant3.Composite;
using Lab4_Trajectory_Variant3.Models;

namespace Lab4_Trajectory_Variant3.Decorators;

/// <summary>Базовый декоратор, сохраняющий общий интерфейс компонента.</summary>
public abstract class TrajectoryComponentDecorator : ITrajectoryComponent
{
    protected ITrajectoryComponent Inner { get; }
    public string Name => Inner.Name;

    protected TrajectoryComponentDecorator(ITrajectoryComponent inner) =>
        Inner = inner ?? throw new ArgumentNullException(nameof(inner));

    public IReadOnlyList<TrajectoryResult> Calculate() => Inner.Calculate();

    public void Display(int indent = 0)
    {
        Inner.Display(indent);
        DisplaySupplement(Calculate(), indent);
    }

    protected abstract void DisplaySupplement(
        IReadOnlyList<TrajectoryResult> results, int indent);
}
