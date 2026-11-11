using Lab6.TrajectoryProxy.Core;
using Lab6.TrajectoryProxy.Models;

namespace Lab6.TrajectoryProxy.Proxy;

/// <summary>
/// Заместитель (Proxy): реализует тот же интерфейс, лениво создаёт
/// реальный сервис и сохраняет результаты для повторных запросов.
/// </summary>
public sealed class TrajectoryCalculationProxy : ITrajectoryService
{
    private ITrajectoryService? _realService;
    private readonly Dictionary<TrajectoryParameters, TrajectoryResult> _cache = new();

    public int CacheHits { get; private set; }
    public int PhysicalCalculations { get; private set; }
    public int CachedResults => _cache.Count;
    public bool IsRealServiceCreated => _realService is not null;

    private ITrajectoryService RealService =>
        _realService ??= new RealTrajectoryService();

    public TrajectoryResult Calculate(TrajectoryParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        if (_cache.TryGetValue(parameters, out var cachedResult))
        {
            CacheHits++;
            return cachedResult;
        }

        var result = RealService.Calculate(parameters);
        _cache.Add(parameters, result);
        PhysicalCalculations++;
        return result;
    }

    /// <summary>Удаляет сохранённые результаты, не сбрасывая статистику.</summary>
    public void ClearCache() => _cache.Clear();
}
