using TrajectoryMovementLab10.Models;

namespace TrajectoryMovementLab10.Observer;

public enum TrajectoryEventType
{
    StateChanged,
    ParametersChanged,
    ResultReady,
    CalculationFailed,
    SessionReset
}

/// <summary>Уведомление, передаваемое всем подписанным наблюдателям.</summary>
public sealed record TrajectoryEvent(
    TrajectoryEventType Type,
    string StateName,
    string Message,
    TrajectoryParameters? Parameters = null,
    TrajectoryResult? Result = null,
    DateTime? OccurredAt = null);
