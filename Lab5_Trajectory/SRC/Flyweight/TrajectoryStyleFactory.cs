namespace Lab5_Trajectory_Variant3.Flyweight;

/// <summary>Хранит и повторно использует экземпляры стилей по ключу.</summary>
public sealed class TrajectoryStyleFactory
{
    private readonly Dictionary<string, TrajectoryStyle> _cache = new(StringComparer.OrdinalIgnoreCase);
    public int CreatedCount => _cache.Count;

    public TrajectoryStyle GetStyle(string key)
    {
        if (_cache.TryGetValue(key, out var existing)) return existing;
        var created = key.ToLowerInvariant() switch
        {
            "wind" => new TrajectoryStyle("wind", "◆", "Модель с ветром", "синий"),
            "high" => new TrajectoryStyle("high", "▲", "Высокая траектория", "зелёный"),
            _ => new TrajectoryStyle("ideal", "●", "Баллистическая модель", "серый")
        };
        _cache[key] = created;
        return created;
    }
}
