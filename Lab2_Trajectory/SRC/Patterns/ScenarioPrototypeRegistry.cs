using TrajectoryMovementLab.Models;

namespace TrajectoryMovementLab.Patterns;

/// <summary>Хранилище именованных прототипов сценариев.</summary>
public sealed class ScenarioPrototypeRegistry
{
    private readonly Dictionary<string, TrajectoryScenario> _prototypes = new();

    public void Register(string key, TrajectoryScenario prototype)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentNullException.ThrowIfNull(prototype);
        _prototypes[key] = prototype.Clone();
    }

    public TrajectoryScenario Create(string key)
    {
        if (!_prototypes.TryGetValue(key, out var prototype))
            throw new KeyNotFoundException($"Прототип '{key}' не найден.");

        return prototype.Clone();
    }
}
