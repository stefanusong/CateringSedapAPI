using CateringSedapAPI.Entitties;
using CateringSedapAPI.Events;

namespace CateringSedapAPI.Aggregates
{
    public class CurrentState
    {
        public ReservationStatus StatusNow { get; set; }
    }

    public class ReservationAggregate
    {
        public Guid ID;
        private readonly IList<IReservationEvent> _allEvents = new List<IReservationEvent>();

        private readonly CurrentState _currentState = new();

        public ReservationAggregate(Guid id)
        {
            ID = id;
        }

        public void CreateReservation(ReservationStatus status)
        {
            AddEvent(new ReservationCreated(ID, status, DateTime.UtcNow));
        }

        public void UpdateReservation(ReservationStatus status)
        {
            AddEvent(new ReservationUpdated(ID, status, DateTime.UtcNow));
        }

        public void Apply(ReservationCreated evnt)
        {
            _currentState.StatusNow = evnt.Status;
        }

        public void Apply(ReservationUpdated evnt)
        {
            _currentState.StatusNow = evnt.Status;
        }

        public void ApplyEvent(IReservationEvent evnt)
        {
            switch(evnt)
            {
                case ReservationCreated createReservation:
                    Apply(createReservation);
                    break;
                case ReservationUpdated updateReservation:
                    Apply(updateReservation);
                    break;
            }

            _allEvents.Add(evnt);
        }

        public void AddEvent(IReservationEvent evnt)
        {
            ApplyEvent(evnt);
        }

        public IList<IReservationEvent> GetAllEvents()
        {
            return new List<IReservationEvent>(_allEvents);
        }

        public ReservationStatus GetStatusNow()
        {
            return _currentState.StatusNow;
        }


    }
}
