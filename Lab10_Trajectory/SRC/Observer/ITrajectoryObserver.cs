namespace TrajectoryMovementLab10.Observer;

/// <summary>Контракт наблюдателя, получающего уведомления об изменениях сессии.</summary>
public interface ITrajectoryObserver
{
    void Update(TrajectoryEvent notification);
}
