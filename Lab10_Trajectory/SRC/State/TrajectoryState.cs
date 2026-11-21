using TrajectoryMovementLab10.Models;

namespace TrajectoryMovementLab10.State;

/// <summary>Общий интерфейс состояний жизненного цикла расчёта траектории.</summary>
public abstract class TrajectoryState
{
    public abstract string Name { get; }

    public virtual void Configure(TrajectorySession session, TrajectoryParameters parameters) =>
        Reject("изменение параметров");

    public virtual void Start(TrajectorySession session) => Reject("запуск расчёта");

    public virtual void Complete(TrajectorySession session, TrajectoryResult result) =>
        Reject("завершение расчёта");

    public virtual void Fail(TrajectorySession session, string error) =>
        Reject("фиксация ошибки");

    public virtual void Reset(TrajectorySession session) => Reject("сброс сессии");

    protected void Reject(string operation) =>
        throw new InvalidOperationException(
            $"Операция «{operation}» недоступна в состоянии «{Name}».");
}
