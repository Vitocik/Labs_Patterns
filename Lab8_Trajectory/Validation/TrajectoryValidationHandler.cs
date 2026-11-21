using Lab8.TrajectoryChainOfResponsibility.Models;

namespace Lab8.TrajectoryChainOfResponsibility.Validation;

/// <summary>Базовый обработчик цепочки обязанностей.</summary>
public abstract class TrajectoryValidationHandler
{
    private TrajectoryValidationHandler? _next;

    public TrajectoryValidationHandler SetNext(TrajectoryValidationHandler next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        return next;
    }

    public TrajectoryValidationResult Handle(TrajectoryParameters? parameters)
        => HandleNext(parameters, []);

    protected abstract string Name { get; }
    protected abstract string? ValidateCurrent(TrajectoryParameters? parameters);

    private TrajectoryValidationResult HandleNext(
        TrajectoryParameters? parameters,
        List<string> passedHandlers)
    {
        var error = ValidateCurrent(parameters);
        if (error is not null)
        {
            return new TrajectoryValidationResult(
                false, Name, error, passedHandlers.AsReadOnly());
        }

        passedHandlers.Add(Name);
        return _next is null
            ? new TrajectoryValidationResult(
                true, string.Empty, "Все проверки успешно пройдены.", passedHandlers.AsReadOnly())
            : _next.HandleNext(parameters, passedHandlers);
    }
}
