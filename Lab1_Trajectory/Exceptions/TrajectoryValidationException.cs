namespace TrajectoryMovementLab.Exceptions;

/// <summary>
/// Ошибка проверки параметров траектории.
/// </summary>
public sealed class TrajectoryValidationException : Exception
{
    public TrajectoryValidationException(IReadOnlyList<string> errors)
        : base(string.Join(" ", errors))
    {
        Errors = errors;
    }

    public IReadOnlyList<string> Errors { get; }
}
