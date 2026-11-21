using Lab8.TrajectoryChainOfResponsibility.Models;
using Lab8.TrajectoryChainOfResponsibility.Validation;

namespace Lab8.TrajectoryChainOfResponsibility.Core;

/// <summary>Сервис-шлюз: допускает расчёт только после успешной работы всей цепочки.</summary>
public sealed class ValidatedTrajectoryService : ITrajectoryService
{
    private readonly TrajectoryValidationPipeline _pipeline;
    private readonly ITrajectoryService _calculationService;

    public ValidatedTrajectoryService()
    {
        _pipeline = new TrajectoryValidationPipeline();
        _calculationService = new RealTrajectoryService();
    }

    public int SuccessfulCalculations { get; private set; }

    public TrajectoryValidationResult Validate(TrajectoryParameters? parameters)
        => _pipeline.Validate(parameters);

    public TrajectoryResult Calculate(TrajectoryParameters parameters)
    {
        var validation = _pipeline.Validate(parameters);
        if (!validation.IsValid)
            throw new TrajectoryValidationException(validation);

        var result = _calculationService.Calculate(parameters);
        SuccessfulCalculations++;
        return result;
    }
}
