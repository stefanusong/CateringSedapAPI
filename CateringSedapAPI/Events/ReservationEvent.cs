using CateringSedapAPI.Entitties;

namespace CateringSedapAPI.Events
{
    public interface IReservationEvent
    {
    }

    public record ReservationCreated(Guid id, ReservationStatus Status, DateTime DateTime) : IReservationEvent;

    public record ReservationUpdated(Guid id, ReservationStatus Status, DateTime DateTime) : IReservationEvent;
}
