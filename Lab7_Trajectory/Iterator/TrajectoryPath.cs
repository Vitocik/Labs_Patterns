using System.Collections;
using Lab7.TrajectoryCommandIterator.Models;

namespace Lab7.TrajectoryCommandIterator.Iterator;

/// <summary>Агрегат точек траектории, предоставляющий независимый итератор.</summary>
public sealed class TrajectoryPath : IEnumerable<TrajectoryPoint>
{
    private readonly IReadOnlyList<TrajectoryPoint> _points;

    public TrajectoryPath(IReadOnlyList<TrajectoryPoint> points)
    {
        _points = points ?? throw new ArgumentNullException(nameof(points));
    }

    public IEnumerator<TrajectoryPoint> GetEnumerator() => new TrajectoryPointIterator(_points);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
