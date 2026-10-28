namespace TrajectoryMovementLab.Patterns;

public interface IPrototype<out T>
{
    T Clone();
}
