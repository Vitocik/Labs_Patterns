using Lab8.TrajectoryChainOfResponsibility.Models;

namespace Lab8.TrajectoryChainOfResponsibility.Validation;

/// <summary>Формирует и запускает стандартную последовательность обработчиков.</summary>
public sealed class TrajectoryValidationPipeline
{
    private readonly TrajectoryValidationHandler _first;

    public TrajectoryValidationPipeline()
    {
        var data = new ScenarioDataHandler();
        var physics = new PhysicalMovementHandler();
        var sampling = new TrajectorySamplingHandler();
        data.SetNext(physics).SetNext(sampling);
        _first = data;
    }

    public TrajectoryValidationResult Validate(TrajectoryParameters? parameters)
        => _first.Handle(parameters);
}
