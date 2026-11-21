namespace Lab8.TrajectoryChainOfResponsibility.Validation;

/// <summary>Результат обработки параметров цепочкой проверок.</summary>
public sealed record TrajectoryValidationResult(
    bool IsValid,
    string FailedHandler,
    string Message,
    IReadOnlyList<string> PassedHandlers);
