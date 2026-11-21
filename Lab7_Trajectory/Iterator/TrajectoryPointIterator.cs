using System.Collections;
using Lab7.TrajectoryCommandIterator.Models;

namespace Lab7.TrajectoryCommandIterator.Iterator;

/// <summary>Конкретный итератор, последовательно обходящий точки траектории.</summary>
public sealed class TrajectoryPointIterator : IEnumerator<TrajectoryPoint>
{
    private readonly IReadOnlyList<TrajectoryPoint> _points;
    private int _index = -1;

    public TrajectoryPointIterator(IReadOnlyList<TrajectoryPoint> points)
    {
        _points = points ?? throw new ArgumentNullException(nameof(points));
    }

    public TrajectoryPoint Current => _index >= 0 && _index < _points.Count
        ? _points[_index]
        : throw new InvalidOperationException("Итератор не находится на допустимой точке.");

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (_index + 1 >= _points.Count)
        {
            _index = _points.Count;
            return false;
        }

        _index++;
        return true;
    }

    public void Reset() => _index = -1;
    public void Dispose() { }
}
