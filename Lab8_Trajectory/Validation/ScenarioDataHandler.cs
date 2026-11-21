using Lab8.TrajectoryChainOfResponsibility.Models;

namespace Lab8.TrajectoryChainOfResponsibility.Validation;

/// <summary>Первый этап: наличие сценария и корректность числовых значений.</summary>
public sealed class ScenarioDataHandler : TrajectoryValidationHandler
{
    protected override string Name => nameof(ScenarioDataHandler);

    protected override string? ValidateCurrent(TrajectoryParameters? p)
    {
        if (p is null)
            return "Параметры сценария не переданы.";
        if (string.IsNullOrWhiteSpace(p.ScenarioName))
            return "Название сценария должно быть задано.";

        var values = new (double Value, string Name)[]
        {
            (p.InitialX, nameof(p.InitialX)),
            (p.InitialY, nameof(p.InitialY)),
            (p.InitialSpeed, nameof(p.InitialSpeed)),
            (p.LaunchAngleDegrees, nameof(p.LaunchAngleDegrees)),
            (p.Gravity, nameof(p.Gravity)),
            (p.TimeStep, nameof(p.TimeStep)),
            (p.WindSpeed, nameof(p.WindSpeed))
        };

        var invalid = values.FirstOrDefault(item => !double.IsFinite(item.Value));
        return values.Any(item => !double.IsFinite(item.Value))
            ? $"Параметр {invalid.Name} должен быть конечным числом."
            : null;
    }
}
