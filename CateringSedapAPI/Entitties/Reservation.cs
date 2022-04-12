namespace CateringSedapAPI.Entitties
{
    public class Reservation
    {
        private Guid Id { get; set; }
        private ReservationStatus Status { get; set; }
        private DateTime ReservationDate { get; set; }
        
    }


    public enum ReservationStatus
    {
        Received = 1,
        Processed,
        Accepted,
        WaitingForPayment,
        Rejected,
        OnDelivery,
        Delivered
    }
}
