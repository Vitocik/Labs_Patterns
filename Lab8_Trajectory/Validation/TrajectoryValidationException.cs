namespace Lab8.TrajectoryChainOfResponsibility.Validation;

/// <summary>Исключение, сохраняющее сведения о точке отказа цепочки.</summary>
public sealed class TrajectoryValidationException : ArgumentException
{
    public TrajectoryValidationException(TrajectoryValidationResult validation)
        : base($"{validation.FailedHandler}: {validation.Message}")
    {
        Validation = validation;
    }

    public TrajectoryValidationResult Validation { get; }
}
