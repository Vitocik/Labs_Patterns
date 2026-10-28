using TrajectoryMovementLab.Bridge;
using TrajectoryMovementLab.Models;
using TrajectoryMovementLab.Validation;

namespace TrajectoryMovementLab.Adapter;

/// <summary>
/// Адаптер превращает современный интерфейс IFlightModel в вызов старого движка:
/// переводит градусы в радианы и преобразует LegacyFlightData в TrajectoryResult.
/// </summary>
public sealed class LegacyTrajectoryAdapter : IFlightModel
{
    private readonly ILegacyTrajectoryEngine _legacyEngine;

    public LegacyTrajectoryAdapter(ILegacyTrajectoryEngine legacyEngine)
    {
        _legacyEngine = legacyEngine ?? throw new ArgumentNullException(nameof(legacyEngine));
    }

    public string Name => "Старая модель через адаптер";

    public TrajectoryResult Calculate(TrajectoryParameters parameters)
    {
        TrajectoryValidator.Validate(parameters);

        double angleRadians = parameters.LaunchAngleDeg * Math.PI / 180.0;
        LegacyFlightData legacy = _legacyEngine.Solve(
            parameters.StartX,
            parameters.StartY,
            parameters.InitialSpeed,
            angleRadians,
            parameters.Gravity,
            parameters.WindSpeed,
            parameters.TimeStep);

        var points = legacy.Samples
            .Select(sample => new TrajectoryPoint(
                sample.SecondsElapsed,
                sample.XCoordinate,
                sample.HeightMeters))
            .ToArray();

        return new TrajectoryResult(
            Name,
            legacy.FlightSeconds,
            legacy.HighestPointY,
            legacy.LandingCoordinateX,
            points);
    }
}
