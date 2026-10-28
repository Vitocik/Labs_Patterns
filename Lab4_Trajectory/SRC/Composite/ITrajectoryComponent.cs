using Lab4_Trajectory_Variant3.Models;

namespace Lab4_Trajectory_Variant3.Composite;

/// <summary>Общий контракт отдельного сценария и группы сценариев.</summary>
public interface ITrajectoryComponent
{
    string Name { get; }
    IReadOnlyList<TrajectoryResult> Calculate();
    void Display(int indent = 0);
}
