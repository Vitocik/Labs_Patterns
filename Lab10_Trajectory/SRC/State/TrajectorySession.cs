using TrajectoryMovementLab10.Calculation;
using TrajectoryMovementLab10.Models;
using TrajectoryMovementLab10.Observer;
using TrajectoryMovementLab10.Validation;

namespace TrajectoryMovementLab10.State;

/// <summary>
/// Context паттерна «Состояние» и субъект паттерна «Наблюдатель».
/// Делегирует допустимые операции текущему состоянию и рассылает события подписчикам.
/// </summary>
public sealed class TrajectorySession
{
    private readonly List<ITrajectoryObserver> _observers = [];
    private TrajectoryState _state = new DraftState();

    public string StateName => _state.Name;
    public TrajectoryParameters? Parameters { get; private set; }
    public TrajectoryResult? Result { get; private set; }
    public string? LastError { get; private set; }

    public void Attach(ITrajectoryObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        if (!_observers.Contains(observer))
            _observers.Add(observer);
    }

    public void Detach(ITrajectoryObserver observer) => _observers.Remove(observer);

    public void Configure(TrajectoryParameters parameters)
    {
        TrajectoryValidator.Validate(parameters);
        _state.Configure(this, parameters);
        Publish(TrajectoryEventType.ParametersChanged,
            $"Параметры сценария «{parameters.Name}» установлены.", parameters: parameters);
    }

    public TrajectoryResult Calculate(TrajectoryCalculationEngine calculator)
    {
        if (Parameters is null)
            throw new InvalidOperationException("Сначала необходимо задать параметры траектории.");

        _state.Start(this);
        try
        {
            TrajectoryResult result = calculator.Calculate(Parameters);
            _state.Complete(this, result);
            return result;
        }
        catch (Exception exception)
        {
            _state.Fail(this, exception.Message);
            throw;
        }
    }

    public void Reset() => _state.Reset(this);

    internal void ApplyParameters(TrajectoryParameters parameters)
    {
        Parameters = parameters;
        Result = null;
        LastError = null;
    }

    internal void ApplyResult(TrajectoryResult result) =>
        Result = result;

    internal void ApplyError(string error) =>
        LastError = error;

    internal void TransitionTo(TrajectoryState nextState)
    {
        string previousName = _state.Name;
        _state = nextState;
        Publish(TrajectoryEventType.StateChanged,
            $"Состояние изменено: «{previousName}» → «{_state.Name}».");
        if (nextState is CompletedState && Result is not null)
        {
            Publish(TrajectoryEventType.ResultReady,
                "Расчёт траектории успешно завершён.", result: Result);
        }
        if (nextState is FailedState)
        {
            Publish(TrajectoryEventType.CalculationFailed,
                $"Расчёт завершился ошибкой: {LastError}");
        }
    }

    internal void ResetToDraft()
    {
        string previousName = _state.Name;
        Parameters = null;
        Result = null;
        LastError = null;
        _state = new DraftState();
        Publish(TrajectoryEventType.StateChanged,
            $"Состояние изменено: «{previousName}» → «{_state.Name}».");
        Publish(TrajectoryEventType.SessionReset, "Сессия сброшена.");
    }

    private void Publish(
        TrajectoryEventType type,
        string message,
        TrajectoryParameters? parameters = null,
        TrajectoryResult? result = null)
    {
        var notification = new TrajectoryEvent(
            type, StateName, message, parameters, result, DateTime.Now);

        foreach (ITrajectoryObserver observer in _observers.ToArray())
            observer.Update(notification);
    }
}
