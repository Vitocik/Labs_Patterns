namespace Lab5_Trajectory_Variant3.Flyweight;

/// <summary>Неизменяемое внутреннее состояние, разделяемое между отображениями.</summary>
public sealed class TrajectoryStyle
{
    public string Key { get; }
    public string Symbol { get; }
    public string Label { get; }
    public string ColorName { get; }

    internal TrajectoryStyle(string key, string symbol, string label, string colorName)
    { Key = key; Symbol = symbol; Label = label; ColorName = colorName; }
}
