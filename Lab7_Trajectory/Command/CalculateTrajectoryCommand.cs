using Lab7.TrajectoryCommandIterator.Core;
using Lab7.TrajectoryCommandIterator.Models;
using Lab7.TrajectoryCommandIterator.Session;

namespace Lab7.TrajectoryCommandIterator.Command;

/// <summary>Конкретная команда расчёта и добавления траектории в сессию.</summary>
public sealed class CalculateTrajectoryCommand : ITrajectoryCommand
{
    private readonly ITrajectoryService _service;
    private readonly TrajectorySession _session;
    private readonly TrajectoryParameters _parameters;
    private bool _isExecuted;

    public CalculateTrajectoryCommand(
        ITrajectoryService service,
        TrajectorySession session,
        TrajectoryParameters parameters)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _session = session ?? throw new ArgumentNullException(nameof(session));
        _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
    }

    public string Description => $"Расчёт сценария «{_parameters.ScenarioName}»";
    public TrajectoryResult? Result { get; private set; }

    public void Execute()
    {
        if (_isExecuted)
            throw new InvalidOperationException("Команда уже выполнена. Сначала отмените её.");

        var calculated = _service.Calculate(_parameters);
        _session.Add(calculated);
        Result = calculated;
        _isExecuted = true;
    }

    public void Undo()
    {
        if (!_isExecuted || Result is null)
            throw new InvalidOperationException("Нельзя отменить команду, которая не была выполнена.");

        _session.Remove(Result);
        _isExecuted = false;
    }
}
