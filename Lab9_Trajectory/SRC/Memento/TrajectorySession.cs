using TrajectoryMovementLab9.Models;

namespace TrajectoryMovementLab9.Memento;

/// <summary>Originator: владеет текущими параметрами и результатом расчёта.</summary>
public sealed class TrajectorySession
{
    public TrajectoryParameters? Parameters { get; private set; }
    public TrajectoryResult? Result { get; private set; }

    public void SetParameters(TrajectoryParameters parameters)
    {
        Parameters = parameters;
        Result = null;
    }

    public void SetResult(TrajectoryResult result)
    {
        Result = result;
    }

    public TrajectoryMemento CreateMemento()
    {
        if (Parameters is null)
            throw new InvalidOperationException("Нельзя сохранить снимок до ввода параметров.");

        return new TrajectoryMemento(Parameters, Result, DateTime.Now);
    }

    public void RestoreMemento(TrajectoryMemento memento)
    {
        ArgumentNullException.ThrowIfNull(memento);
        Parameters = memento.Parameters;
        Result = memento.Result;
    }
}
