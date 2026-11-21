using TrajectoryMovementLab9.Calculation;
using TrajectoryMovementLab9.Memento;
using TrajectoryMovementLab9.Models;
using TrajectoryMovementLab9.Validation;

namespace TrajectoryMovementLab9.Mediator;

/// <summary>
/// ConcreteMediator: координирует ввод, расчёт, вывод результатов и историю снимков.
/// Коллеги не обращаются друг к другу напрямую.
/// </summary>
public sealed class TrajectoryMediator : ITrajectoryMediator
{
    private readonly TrajectoryCalculationEngine _calculator = new();
    private readonly TrajectorySession _session = new();
    private readonly TrajectoryHistory _history = new();
    private readonly TrajectoryResultView _resultView = new();

    public TrajectoryMediator()
    {
        Input = new TrajectoryInput(this);
        HistoryPanel = new TrajectoryHistoryPanel(this);
    }

    public TrajectoryInput Input { get; }
    public TrajectoryHistoryPanel HistoryPanel { get; }

    public void ParametersSubmitted(TrajectoryParameters parameters)
    {
        TrajectoryValidator.Validate(parameters);
        _session.SetParameters(parameters);
        _resultView.ShowParameters(parameters);
    }

    public void CalculationRequested()
    {
        if (_session.Parameters is null)
            throw new InvalidOperationException("Сначала необходимо задать параметры движения.");

        TrajectoryResult result = _calculator.Calculate(_session.Parameters);
        _session.SetResult(result);
        _resultView.ShowResult(result);
    }

    public void SnapshotSaveRequested()
    {
        int number = _history.Save(_session.CreateMemento());
        _resultView.ShowMessage($"Снимок №{number} сохранён. Всего снимков: {_history.Count}.");
    }

    public void SnapshotRestoreRequested(int number)
    {
        TrajectoryMemento snapshot = _history.Get(number);
        _session.RestoreMemento(snapshot);
        _resultView.ShowMessage($"Восстановлено состояние из снимка №{number}: {snapshot.Description}.");
        if (_session.Result is not null)
            _resultView.ShowResult(_session.Result);
    }

    public void SnapshotListRequested()
    {
        _resultView.ShowMessage("История снимков:");
        foreach (string description in _history.DescribeAll())
            _resultView.ShowMessage($"  {description}");
        if (_history.Count == 0)
            _resultView.ShowMessage("  История пока пуста.");
    }
}
